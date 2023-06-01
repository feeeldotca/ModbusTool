using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusTool
{
    public partial class FrmModbusRtu : Form
    {        
        // Create a enum of data format
            public enum DataFormat
        {
            ABCD, BADC, CDAB, DCBA
        }

        // Create an enum of Data Storage area
        public enum StoreArea
        {
            OutputCoil_0X,
            InputState_1X,
            OutputRegister_4X,
            InputRegister_3X,
        }

        // Create an enum of variable type
        public enum VarType
        {
            Bit,
            Byte, 
            Short,
            UShort,
            Int,
            UInt,
            Float
        }

        public FrmModbusRtu()
        {
            InitializeComponent();
            this.Load += FrmModbusRtu_Load;
        }
        // a flag of rtu connection
        private bool isConnected = false;

        ModbusRtu modbusRtu = new ModbusRtu();
        // Modbus modbus = null;
        private void FrmModbusRtu_Load(object sender, EventArgs e)
        {  

            // initialize controls

            // port number:
            string[] PortList = SerialPort.GetPortNames();
            if(PortList != null)
            {
                this.cmb_Port.Items.AddRange(PortList);
                this.cmb_Port.SelectedIndex = 5;
            }

            // Baudrate:

            this.cmb_Paud.DataSource = new string[] { "2400", "4800", "9600", "19200", "38400" };
            this.cmb_Paud.SelectedIndex = 2;

            // Pariaty
            //enum Parity = { "None" ,"Odd", "Even", "Mark", "Space" };
            this.cmb_Parity.DataSource = Enum.GetNames(typeof(Parity));
            this.cmb_Parity.SelectedIndex = 0;

            // StopBits {"None", "One", "Two", "OnePointFive"}
            this.cmb_Stopbits.DataSource = Enum.GetNames(typeof(StopBits));
            this.cmb_Stopbits.SelectedIndex = 1;

            // DataFormat:
            this.cmb_Dataformat.DataSource = Enum.GetNames(typeof(DataFormat));
            this.cmb_Dataformat.SelectedIndex = 0;

            // Store Area: 
            this.cmb_Storage.DataSource = Enum.GetNames(typeof(StoreArea));
            this.cmb_Storage.SelectedIndex = 0;

            //Variable Type:
            this.cmb_VarType.DataSource = Enum.GetNames(typeof(VarType));
            this.cmb_VarType.SelectedIndex = 0;
            
        }

         // get system time in string format to be used in log information
         private string CurrentTime { get { return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); } }
           
        // prepare log info
        /// <summary>
        /// General method to record log info
        /// </summary>
        /// <param name="logInfo"> infomation to be logged </param>
        /// <param name="imgCode">the image list index</param>
        private void AddLog(string logInfo, int imgCode)
        {
            ListViewItem lst = new ListViewItem("  " + CurrentTime, imgCode);
            lst.SubItems.Add(logInfo);
            lst_Info.Items.Insert(0, lst);
        }

        /// <summary>
        /// Click connect button will triger this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(isConnected)
            {
                AddLog("Modbus connected already, please don't reconnect",1 );
                return;
            }
            try
            {
                modbusRtu.OpenMyCom(int.Parse(this.cmb_Paud.Text.Trim()), this.cmb_Port.Text.Trim(), int.Parse(this.tbDatabits.Text.Trim()), (Parity)Enum.Parse(typeof(Parity), this.cmb_Parity.SelectedItem.ToString(),false), (StopBits)Enum.Parse(typeof(StopBits), this.cmb_Stopbits.Text.Trim())) ;

                //modbus = new Modbus(this.cmb_Port.Text.Trim(), int.Parse(this.cmb_Paud.Text.Trim()), (Parity)Enum.Parse(typeof(Parity), this.cmb_Parity.SelectedItem.ToString(), false), int.Parse(this.tbDatabits.Text.Trim()), (StopBits)Enum.Parse(typeof(StopBits), this.cmb_Stopbits.Text.Trim()));
            }
            catch(Exception ex)
            {
                isConnected = false;
                AddLog("ModbusRTU connection failed: " + ex.Message, 1);
                return;
            }

            isConnected = true;
            AddLog("ModbusRTU successfully connected", 0);
        }

        // disconnect Modbus Connection
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            modbusRtu.CloseMyCom();
            
            isConnected = false;
            AddLog("ModbusRTU disconnected.", 0);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                AddLog("Please check Modbus connection", 1);
                return;
            }

            ushort slaveAddress = 0;
            ushort startAddress = 0;
            ushort rwLength = 0;
            //byte slave = 0;

            if(!ushort.TryParse(this.txt_SlaveAdd.Text.Trim(), out slaveAddress))
            {
                AddLog("Read failed, Slave address need to be positive interger", 1);
                return;
            }
            /*
            if (!Byte.TryParse(this.txt_SlaveAdd.Text.Trim(), out slave))
            {
                AddLog("Read failed, Slave address need to be positive interger", 1);
                return;
            }*/
            if (!ushort.TryParse(this.txt_Address.Text.Trim(), out startAddress))
            {
                AddLog("Read failed, Start address need to be positive interger", 1);
                return;
            }
            if (!ushort.TryParse(this.txt_Length.Text.Trim(), out rwLength))
            {
                AddLog("Read failed, Read/Write length need to be positive interger", 1);
                return;
            }
            
            // Read data
            // get selected variable's type 
            VarType varType = (VarType)(Enum.Parse(typeof(VarType), this.cmb_VarType.Text.Trim()));

            // get selected storage area
            StoreArea storeArea = (StoreArea)(Enum.Parse(typeof(StoreArea), this.cmb_Storage.Text.Trim()));
            
            // create bype array
            byte[] result = null;
            // this is for other ChatGPT created code test: bool[] result1 = null; 
            switch (varType)
            {
                case VarType.Bit:
                    switch (storeArea)
                    {
                        case StoreArea.OutputCoil_0X:
                            result = modbusRtu.ReadOutputCoil(slaveAddress, startAddress, rwLength);
                            //result1 = modbus.ReadOutputCoilStatus(slave, startAddress, rwLength);
                            break;
                       
                        case StoreArea.InputState_1X:
                            result = modbusRtu.ReadInputStatus(slaveAddress, startAddress, rwLength);
                            break;
                        
                        case StoreArea.InputRegister_3X:
                        case StoreArea.OutputRegister_4X:
                            AddLog("Read failed, storage type not correct", 1);
                            return;

                        default:
                            break;
                    }
                    string binaryString = string.Empty;
                    if(result != null)
                    {
                        //var item1 = "";
                        foreach (var item in result)
                        {
                            //item1 = item == true ? "1" : "0";
                            char[] c = Convert.ToString(item, 2).PadLeft(8, '0').ToCharArray();
                            Array.Reverse(c);
                            binaryString += new string(c);
                        }
                        AddLog("Read Successful, the result is " + binaryString.Substring(0, rwLength), 0); 
                    }
                    else
                    {
                        AddLog("Read failed, please check connection, address or variable type", 1);
                    }
                    break;
                case VarType.Short:
                    switch (storeArea)
                    {
                        case StoreArea.OutputCoil_0X:
                        case StoreArea.InputState_1X:
                            AddLog("Read error, storage area type not correct", 1);
                            break;
                        case StoreArea.OutputRegister_4X:
                            result = modbusRtu.ReadKeepReg(slaveAddress, startAddress, rwLength);
                            break;
                        case StoreArea.InputRegister_3X:
                            result = modbusRtu.ReadInputReg(slaveAddress, startAddress, rwLength);
                            break;
                        default:
                            break;
                    }
                    string shortString = string.Empty;

                    if (result != null && result.Length == rwLength * 2)
                    {
                        for (int i = 0; i < result.Length; i += 2)
                        {
                            shortString += BitConverter.ToInt16(Get16ByteArray(result, i, 
                                modbusRtu.Data_Format), 0).ToString() + " ";
                        }
                        AddLog("读取成功，结果为" + shortString.Trim(), 0);
                    }
                    else
                    {
                        AddLog( "读取失败，请检查地址、类型或连接状态", 1);
                    }
                    break;
                case VarType.UShort:
                    break;
                case VarType.Int:
                    break;
                case VarType.UInt:
                    break;
                case VarType.Float:
                    break;
                default:
                    break;
            }
        }

        #region get data from all kinds of array

        private byte[] Get16ByteArray(byte[] byteArray, int start, DataFormat type)
        {
            byte[] Res = new byte[2];

            if (byteArray != null && byteArray.Length >= start + 2)
            {
                byte[] ResTemp = new byte[2];
                for (int i = 0; i < 2; i++)
                {
                    ResTemp[i] = byteArray[i + start];
                }

                switch (type)
                {
                    case DataFormat.ABCD:
                    case DataFormat.CDAB:
                        Res[0] = ResTemp[1];
                        Res[1] = ResTemp[0];
                        break;
                    case DataFormat.BADC:
                    case DataFormat.DCBA:
                        Res = ResTemp;
                        break;
                }
                return Res;
            }
            else
            {
                return null;
            }
        }

        private byte[] Get32ByteArray(byte[] byteArray, int start, DataFormat type)
        {
            byte[] Res = new byte[4];

            if (byteArray != null && byteArray.Length >= start + 4)
            {
                byte[] ResTemp = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    ResTemp[i] = byteArray[i + start];
                }

                switch (type)
                {
                    case DataFormat.ABCD:
                        Res[0] = ResTemp[3];
                        Res[1] = ResTemp[2];
                        Res[2] = ResTemp[1];
                        Res[3] = ResTemp[0];
                        break;
                    case DataFormat.CDAB:
                        Res[0] = ResTemp[1];
                        Res[1] = ResTemp[0];
                        Res[2] = ResTemp[3];
                        Res[3] = ResTemp[2];
                        break;
                    case DataFormat.BADC:
                        Res[0] = ResTemp[2];
                        Res[1] = ResTemp[3];
                        Res[2] = ResTemp[0];
                        Res[3] = ResTemp[1];
                        break;
                    case DataFormat.DCBA:
                        Res = ResTemp;
                        break;
                }
                return Res;
            }
            else
            {
                return null;
            }
        }


        #endregion
    }
}

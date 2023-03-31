using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO.Ports;
using System.Linq;
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
        public FrmModbusRtu()
        {
            InitializeComponent();
            this.Load += FrmModbusRtu_Load;
        }

        private bool isConnect = false;

        private void FrmModbusRtu_Load(object sender, EventArgs e)
        {  

            // initialize controls

            // port number:
            string[] PortList = SerialPort.GetPortNames();
            if(PortList != null)
            {
                this.cmb_Port.Items.AddRange(PortList);
                this.cmb_Port.SelectedIndex = 0;
            }

            // Baudrate:

            this.cmb_Paud.DataSource = new string[] { "2400", "4800", "9600", "19200", "38400" };

            // Pariaty
            //enum Parity = { "Odd", "Even", "None" };
            this.cmb_Parity.DataSource = Enum.GetNames(typeof(Parity));

            // StopBits
            this.cmb_Stopbits.DataSource = Enum.GetNames(typeof(StopBits));
            this.cmb_Parity.SelectedIndex = 1;

            // DataFormat:
            this.cmb_Dataformat.DataSource = Enum.GetNames(typeof(DataFormat));

            // Store Area: 
            this.cmb_Storage.DataSource = Enum.GetNames(typeof(StoreArea));

        }

         // get system time in string format
         private string CurrentTime { get { return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); } }
           
        // prepare log info
        private void AddLog(string logInfo, int imgCode)
        {
            ListViewItem lst = new ListViewItem("  " + CurrentTime, imgCode);
            lst.SubItems.Add(logInfo);
            lst_Info.Items.Insert(0, lst);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(isConnect)
            {
                AddLog("Modbus connected already, please don't reconnect",1 );
                return;
            }
            (int.Parse(this.cmb_Paud.Text.Trim()))
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}

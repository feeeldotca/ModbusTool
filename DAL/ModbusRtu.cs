using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ModbusRtu
    {
        // Instance com port object
        private SerialPort myCom = null;

        // setup timeout attribute value
        public int ReadTimeOut { get; set; } = 2000;

        public int WriteTimeOut { get; set; } = 2000;

        // establish connection and disconnect
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iBaudRate">BaudRate</param>
        /// <param name="iPortName">Port Name</param>
        /// <param name="iDataBits">Data Bits</param>
        /// <param name="iParity">Parity</param>
        /// <param name="iStopBits">Stop bits</param>
        public void OpenMyCom(int iBaudRate, string iPortName, int iDataBits, Parity iParity, StopBits iStopBits)
        {
            // if current com port is open, close it first
            if (myCom.IsOpen)
            {
                myCom.Close();
            }

            //open serial port
            myCom = new SerialPort(iPortName, iBaudRate, iParity, iDataBits, iStopBits);

            // set timeout to be same as property value
            myCom.ReadTimeout = this.ReadTimeOut;
            myCom.WriteTimeout = this.WriteTimeOut;

            myCom.Open();
        }

        // close serial port 
        

        // Read Output Coil status



    }
}

using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    #region class by XingeEdu
    public class ModbusRtu
    {
        #region objects or properties

        // Instance com port object
        private SerialPort myCom = new SerialPort();

        // setup timeout attribute value
        public int ReadTimeOut { get; set; } = 2000;

        public int WriteTimeOut { get; set; } = 2000;

        // read return information timeout
        public int ReceiveTimeOut { get; set; } = 2000;

        //Sleep time 
        public int SleepTime { get; set; } = 20;

        //data format setting:
        public DataFormat DataFormat { get; set; } = DataFormat.ABCD;

        #endregion

        #region open or close serial port
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
        public void CloseMyCom()
        {
            if (myCom.IsOpen)
            {
                myCom.Close();
            }
        }
        #endregion

        #region Read output coil function code 01H
        // Read Output Coil status
        public byte[] ReadOutputCoil(int iDevAdd, int iAddress, int iLength)
        {
            // prepare data to be sent to output coil address
            //byte[] SendCmd = new byte[8];
            //SendCmd[0] = (byte)iDevAdd;
            //SendCmd[1] = 0x01;
            //SendCmd[2] = (byte)(iAddress / 256);
            //SendCmd[3] = (byte)(iAddress % 256);
            //SendCmd[4] = (byte)(iLength / 256);
            //SendCmd[5] = (byte)(iLength % 256);
            //byte[] crc = Crc16(SendCmd, 6);
            //SendCmd[6] = crc[0];
            //SendCmd[7] = crc[1];

            // use ByteArray class to send command
            ByteArray command = new ByteArray();
            command.Add(new byte[]
            {
                (byte)iDevAdd, 0x01, (byte)(iAddress / 256), (byte)(iAddress % 256)
        });
            command.Add(new byte[]
            {
                (byte)(iLength / 256), (byte)(iLength % 256)
            }) ;
            command.Add(Crc16(command.array, 6));
            byte[] Response = new byte[1024];
            // send command data to coil address and get response data
            int byteLength = 0;
            if(iLength % 8 == 0)
            {
                byteLength = iLength / 8;
            }
            else
            {
                byteLength = iLength / 8 + 1;
            }

            // step 2. Send Command and Read response data from buffer

            byte[] response = new byte[byteLength + 5];
            if(SendData(command.array, ref response))
            {
            // step 3. analyse response data 
                return GetByteArray(response, 3, byteLength);
            }
            else
            {
                return null;
            }
                       
        }
        #endregion


        #region Read input coil function code 02H
        /// <summary>
        /// Read input coil
        /// </summary>
        /// <param name="iDevAdd">slave Address</param>
        /// <param name="iAddress">start address</param>
        /// <param name="iLength">read length</param>
        /// <returns></returns>
        public byte[] ReadInputStatus(int iDevAdd, int iAddress, int iLength)
        {
            //Step 1: concat command

            ByteArray SendCommand = new ByteArray();

            SendCommand.Add(new byte[] { (byte)iDevAdd, 0x02, (byte)(iAddress / 256), (byte)(iAddress % 256) });
            SendCommand.Add(new byte[] { (byte)(iLength / 256), (byte)(iLength % 256) });
            SendCommand.Add(Crc16(SendCommand.array, 6));

            //Step 2: Send/Receive command

            int byteLength = 0;
            if (iLength % 8 == 0)
            {
                byteLength = iLength / 8;
            }
            else
            {
                byteLength = iLength / 8 + 1;
            }

            byte[] response = new byte[5 + byteLength];

            if (SendData(SendCommand.array, ref response))
            {
                //Step 3: Analyze command

                if (response[1] == 0x02 && response[2] == byteLength)
                {
                    return GetByteArray(response, 3, byteLength);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Read Keep Register function code 03H

        public byte[] ReadKeepReg(int iDevAdd, int iAddress, int iLength)
        {
            //Step 1: concatinate send command

            ByteArray SendCommand = new ByteArray();

            SendCommand.Add(new byte[] { (byte)iDevAdd, 0x03, (byte)(iAddress / 256), (byte)(iAddress % 256) });
            SendCommand.Add(new byte[] { (byte)(iLength / 256), (byte)(iLength % 256) });
            SendCommand.Add(Crc16(SendCommand.array, 6));

            //Step 2: Send / Receive Command

            int byteLength = iLength * 2;

            byte[] response = new byte[5 + byteLength];

            if (SendData(SendCommand.array, ref response))
            {
                //Step 3: analize command

                if (response[1] == 0x03 && response[2] == byteLength)
                {
                    return GetByteArray(response, 3, byteLength);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region Read Input Register Function code 04H

        public byte[] ReadInputReg(int iDevAdd, int iAddress, int iLength)
        {
            // Step 1: concatinate send command

            ByteArray SendCommand = new ByteArray();

            SendCommand.Add(new byte[] { (byte)iDevAdd, 0x04, (byte)(iAddress / 256), (byte)(iAddress % 256) });
            SendCommand.Add(new byte[] { (byte)(iLength / 256), (byte)(iLength % 256) });
            SendCommand.Add(Crc16(SendCommand.array, 6));

            //Step 2: Send / Receive Command

            int byteLength = iLength * 2;

            byte[] response = new byte[5 + byteLength];

            if (SendData(SendCommand.array, ref response))
            {
                //Step 3: analize command

                if (response[1] == 0x04 && response[2] == byteLength)
                {
                    return GetByteArray(response, 3, byteLength);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 强制单线圈 功能码05H

        public bool ForceCoil(int iDevAdd, int iAddress, bool SetValue)
        {

            //第一步：拼接报文

            ByteArray SendCommand = new ByteArray();

            SendCommand.Add(new byte[] { (byte)iDevAdd, 0x05, (byte)(iAddress / 256), (byte)(iAddress % 256) });
            if (SetValue)
            {
                SendCommand.Add(new byte[] { 0xFF, 0x00 });
            }
            else
            {
                SendCommand.Add(new byte[] { 0x00, 0x00 });
            }
            SendCommand.Add(Crc16(SendCommand.array, 6));

            //第二步：发送报文  接受报文

            byte[] response = new byte[8];

            if (SendData(SendCommand.array, ref response))
            {
                //第三步：验证报文
                return ByteArrayEquals(SendCommand.array, response);
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 预置单个寄存器 功能码06H

        public bool PreSetSingleReg(int iDevAdd, int iAddress, short SetValue)
        {
            //第一步：拼接报文

            ByteArray SendCommand = new ByteArray();

            SendCommand.Add(new byte[] { (byte)iDevAdd, 0x06, (byte)(iAddress / 256), (byte)(iAddress % 256) });

            SendCommand.Add(GetByteArrayFrom16Bit(SetValue));

            SendCommand.Add(Crc16(SendCommand.array, 6));

            //第二步：发送报文  接受报文

            byte[] response = new byte[8];

            if (SendData(SendCommand.array, ref response))
            {
                //第三步：验证报文
                return ByteArrayEquals(SendCommand.array, response);
            }
            else
            {
                return false;
            }
        }

        public bool PreSetSingleReg(int iDevAdd, int iAddress, ushort SetValue)
        {
            //第一步：拼接报文

            ByteArray SendCommand = new ByteArray();

            SendCommand.Add(new byte[] { (byte)iDevAdd, 0x06, (byte)(iAddress / 256), (byte)(iAddress % 256) });

            SendCommand.Add(GetByteArrayFrom16Bit(SetValue));

            SendCommand.Add(Crc16(SendCommand.array, 6));

            //第二步：发送报文  接受报文

            byte[] response = new byte[8];

            if (SendData(SendCommand.array, ref response))
            {
                //第三步：验证报文
                return ByteArrayEquals(SendCommand.array, response);
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region force multiple coil function code 0FH

        public bool ForceMultiCoil(int iDevAdd, int iAddress, bool[] SetValue)
        {
            //Step 1: concat command

            byte[] iSetValue = BoolArrayToByteArray(SetValue);

            ByteArray SendCommand = new ByteArray();

            SendCommand.Add(new byte[] { (byte)iDevAdd, 0x0F, (byte)(iAddress / 256), (byte)(iAddress % 256) });

            SendCommand.Add(new byte[] { (byte)(SetValue.Length / 256), (byte)(SetValue.Length % 256) });

            SendCommand.Add((byte)iSetValue.Length);

            SendCommand.Add(iSetValue);

            SendCommand.Add(Crc16(SendCommand.array, 7 + iSetValue.Length));

            //Step 2: Send / Receive command
            byte[] response = new byte[8];

            if (SendData(SendCommand.array, ref response))
            {
                // Step 3: Command verification, verify if the first 6 bytes correctly, then verify if CRC correct 

                byte[] b = GetByteArray(response, 0, 6);

                byte[] crc = Crc16(b, 6);

                return ByteArrayEquals(GetByteArray(SendCommand.array, 0, 6), b) && crc[0] == response[6] && crc[1] == response[7];

            }

            return false;
        }

        #endregion

        #region 预置多个寄存器 功能码10H

        //浮点型   Int32  UInt32    浮点型数组   Int32数组  UInt32数组  浮点型/int16/uint16

        public bool PreSetMultiByteArray(int iDevAdd, int iAddress, byte[] SetValue)
        {
            if (SetValue == null || SetValue.Length == 0 || SetValue.Length % 2 == 1)
            {
                return false;
            }

            int RegLength = SetValue.Length / 2;

            //第一步：拼接报文

            ByteArray SendCommand = new ByteArray();

            SendCommand.Add(new byte[] { (byte)iDevAdd, 0x10, (byte)(iAddress / 256), (byte)(iAddress % 256) });

            //寄存器数量
            SendCommand.Add(new byte[] { (byte)(RegLength / 256), (byte)(RegLength % 256) });

            //字节数量
            SendCommand.Add((byte)SetValue.Length);

            //具体字节
            SendCommand.Add(SetValue);

            //CRC
            SendCommand.Add(Crc16(SendCommand.array, 7 + SetValue.Length));

            //第二步：发送报文  接受报文

            byte[] response = new byte[8];

            if (SendData(SendCommand.array, ref response))
            {
                //第三步：报文验证   验证前6个字节是否正确，再验证CRC是否正确

                byte[] b = GetByteArray(response, 0, 6);

                byte[] crc = Crc16(b, 6);

                return ByteArrayEquals(GetByteArray(SendCommand.array, 0, 6), b) && crc[0] == response[6] && crc[1] == response[7];
            }
            else
            {
                return false;
            }
        }

        public bool PreSetMultiReg(int iDevAdd, int iAddress, float SetValue)
        {
            return PreSetMultiByteArray(iDevAdd, iAddress, GetByteArrayFrom32Bit(SetValue));
        }

        public bool PreSetMultiReg(int iDevAdd, int iAddress, int SetValue)
        {
            return PreSetMultiByteArray(iDevAdd, iAddress, GetByteArrayFrom32Bit(SetValue));
        }

        public bool PreSetMultiReg(int iDevAdd, int iAddress, uint SetValue)
        {
            return PreSetMultiByteArray(iDevAdd, iAddress, GetByteArrayFrom32Bit(SetValue));
        }

        public bool PreSetMultiReg(int iDevAdd, int iAddress, float[] SetValue)
        {
            ByteArray bSetValue = new ByteArray();

            foreach (float item in SetValue)
            {
                bSetValue.Add(GetByteArrayFrom32Bit(item));
            }

            return PreSetMultiByteArray(iDevAdd, iAddress, bSetValue.array);
        }

        public bool PreSetMultiReg(int iDevAdd, int iAddress, int[] SetValue)
        {
            ByteArray bSetValue = new ByteArray();

            foreach (int item in SetValue)
            {
                bSetValue.Add(GetByteArrayFrom32Bit(item));
            }

            return PreSetMultiByteArray(iDevAdd, iAddress, bSetValue.array);
        }

        public bool PreSetMultiReg(int iDevAdd, int iAddress, short[] SetValue)
        {
            ByteArray bSetValue = new ByteArray();

            foreach (short item in SetValue)
            {
                bSetValue.Add(GetByteArrayFrom16Bit(item));
            }

            return PreSetMultiByteArray(iDevAdd, iAddress, bSetValue.array);
        }

        public bool PreSetMultiReg(int iDevAdd, int iAddress, uint[] SetValue)
        {
            ByteArray bSetValue = new ByteArray();

            foreach (uint item in SetValue)
            {
                bSetValue.Add(GetByteArrayFrom32Bit(item));
            }

            return PreSetMultiByteArray(iDevAdd, iAddress, bSetValue.array);
        }

        public bool PreSetMultiReg(int iDevAdd, int iAddress, ushort[] SetValue)
        {
            ByteArray bSetValue = new ByteArray();

            foreach (ushort item in SetValue)
            {
                bSetValue.Add(GetByteArrayFrom16Bit(item));
            }

            return PreSetMultiByteArray(iDevAdd, iAddress, bSetValue.array);
        }


        #endregion

        #region common methods
        private byte[] GetByteArray(byte[] dest, int offset, int count)
        {
            byte[] res = new byte[count];
            if(dest != null && dest.Length >= offset + count)
            {
                for(int i = 0; i<count; i++)
                {
                    res[i] = dest[offset + i];
                }
                return res;
            }
            else
            {
                return null;
            }
        }

        // step 1. Send command to coil address
        private bool SendData(byte[] cmd, ref byte[] res)
        {
            // Send command to coil address
            
            try{
                
                myCom.Write(cmd, 0, cmd.Length);
                // prepare momery to store response data
                MemoryStream ms = new MemoryStream();

                //prepare a buffer which is big enough to contain response from coil
                byte[] buffer = new byte[1024];

                // get the start time for reading response
                DateTime start = DateTime.Now;


                // read response data from buffer
                // step 1: get the value of current buffer size, if yes then read it and write to memory

                // step 2: if current buffer size is 0 means no more value to read

                // step 3: if tried to read but get nothing then check Time out 
                while (true)
                {
                    if (myCom.BytesToRead >= 1)
                    {
                        int spCount = myCom.Read(buffer, 0, buffer.Length);
                        ms.Write(buffer, 0, spCount);
                    }
                    else
                    {
                        // time out?
                        if ((DateTime.Now - start).TotalMilliseconds > this.ReadTimeOut)
                        {
                            ms.Dispose();
                            return false;
                        }
                        else if (ms.Length > 0)
                            break;
                    }

                }

                res = ms.ToArray();
                ms.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        #endregion

        #region  CRC Verification

        private static readonly byte[] aucCRCHi = {
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40
         };
        private static readonly byte[] aucCRCLo = {
             0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7,
             0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E,
             0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9,
             0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC,
             0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
             0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32,
             0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D,
             0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38,
             0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF,
             0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
             0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1,
             0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4,
             0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB,
             0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
             0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
             0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0,
             0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97,
             0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E,
             0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89,
             0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
             0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83,
             0x41, 0x81, 0x80, 0x40
         };
        private byte[] Crc16(byte[] pucFrame, int usLen)
        {
            int i = 0;
            byte[] res = new byte[2] { 0xFF, 0xFF };

            UInt16 iIndex = 0x0000;

            while (usLen-- > 0)
            {
                iIndex = (UInt16)(res[0] ^ pucFrame[i++]);
                res[0] = (byte)(res[1] ^ aucCRCHi[iIndex]);
                res[1] = aucCRCLo[iIndex];
            }
            return res;
        }

        #endregion

        #region 判断两个数组是否完全一样

        private bool ByteArrayEquals(byte[] b1, byte[] b2)
        {
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i])
                {
                    return false;
                }
            }
            return true;
        }


        #endregion

        #region 布尔数组转换成字节数组

        private byte[] BoolArrayToByteArray(bool[] val)
        {
            if (val == null && val.Length == 0) return null;

            int iByteArrLen = 0;

            if (val.Length % 8 == 0)
            {
                iByteArrLen = val.Length / 8;
            }
            else
            {
                iByteArrLen = val.Length / 8 + 1;
            }

            byte[] result = new byte[iByteArrLen];

            //遍历每个字节
            for (int i = 0; i < iByteArrLen; i++)
            {
                int total = val.Length < 8 * (i + 1) ? val.Length - 8 * i : 8;

                //遍历当前字节的每个位赋值
                for (int j = 0; j < total; j++)
                {
                    result[i] = SetbitValue(result[i], j + 1, val[8 * i + j]);
                }
            }
            return result;
        }

        #endregion

        #region 16位类型转换成字节数组

        private byte[] GetByteArrayFrom16Bit(short SetValue)
        {
            byte[] ResTemp = BitConverter.GetBytes(SetValue);
            byte[] Res = new byte[2];

            switch (this.DataFormat)
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
                default:
                    break;
            }
            return Res;
        }

        private byte[] GetByteArrayFrom16Bit(ushort SetValue)
        {
            byte[] ResTemp = BitConverter.GetBytes(SetValue);
            byte[] Res = new byte[2];

            switch (this.DataFormat)
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
                default:
                    break;
            }
            return Res;
        }

        #endregion

        #region 32位类型转换成字节数组

        private byte[] GetByteArrayFrom32Bit(float SetValue)
        {
            byte[] ResTemp = BitConverter.GetBytes(SetValue);

            byte[] Res = new byte[4];

            switch (this.DataFormat)
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

        private byte[] GetByteArrayFrom32Bit(int SetValue)
        {
            byte[] ResTemp = BitConverter.GetBytes(SetValue);

            byte[] Res = new byte[4];

            switch (this.DataFormat)
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

        private byte[] GetByteArrayFrom32Bit(uint SetValue)
        {
            byte[] ResTemp = BitConverter.GetBytes(SetValue);

            byte[] Res = new byte[4];

            switch (this.DataFormat)
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


        #endregion
    }
    #endregion



    // CRC calculation by ChatGPT

    #region class by ChatGPT

    public class Modbus
    {
        private SerialPort port;

        public Modbus(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        }

        public bool[] ReadOutputCoilStatus(byte slaveAddress, ushort startAddress, ushort quantity)
        {
            // Build Modbus request frame
            byte[] requestFrame = new byte[8];
            requestFrame[0] = slaveAddress;
            requestFrame[1] = 0x01;
            requestFrame[2] = (byte)(startAddress >> 8);
            requestFrame[3] = (byte)(startAddress & 0xFF);
            requestFrame[4] = (byte)(quantity >> 8);
            requestFrame[5] = (byte)(quantity & 0xFF);
            byte[] crc = CalculateCrc(requestFrame, 6);
            requestFrame[6] = crc[0];
            requestFrame[7] = crc[1];

            // Send Modbus request frame to serial port
            if(!port.IsOpen)             port.Open();
            port.Write(requestFrame, 0, requestFrame.Length);

            // Read Modbus response frame from serial port
            byte[] responseFrame = new byte[5 + (int)Math.Ceiling((double)quantity / 8)];
            try
            {
                int bytesRead = port.Read(responseFrame, 0, responseFrame.Length);
                if (bytesRead != responseFrame.Length)
                throw new Exception("Modbus response frame has incorrect length");

            if (responseFrame[0] != slaveAddress)
                throw new Exception("Modbus response frame has incorrect slave address");

            if (responseFrame[1] != 0x01)
                throw new Exception("Modbus response frame has incorrect function code");
            }
            catch
            {
                throw new Exception("Modbus response false");
            }
            
            port.Close();

            // Validate Modbus response frame
           

            byte[] crcResponse = CalculateCrc(responseFrame, responseFrame.Length - 2);
            if (responseFrame[responseFrame.Length - 2] != crcResponse[0] || responseFrame[responseFrame.Length - 1] != crcResponse[1])
                throw new Exception("Modbus response frame has incorrect CRC");

            // Extract output coil status data from Modbus response frame
            bool[] outputCoilStatus = new bool[quantity];
            for (int i = 0; i < quantity; i++)
            {
                byte coilByte = responseFrame[3 + i / 8];
                byte coilBit = (byte)(i % 8);
                outputCoilStatus[i] = (coilByte & (1 << coilBit)) != 0;
            }

            return outputCoilStatus;
        }


        private byte[] CalculateCrc(byte[] data, int length)
        {
            // setup crc initial value to be 16 bit ushort interger
            ushort crc = 0xFFFF;

            // use the initial value to XOR every bit of the data
            for (int i = 0; i < length; i++)
            {
                crc ^= data[i];
                for (int j = 0; j < 8; j++)
                {
                    // check if the LSB is 0 or 1, 
                    bool bit = (crc & 0x0001) != 0;
                    // if LSB is 0, only do right shift one bit
                    crc >>= 1;
                    // if LSB is 1, right shift one bit then XOR with 0xA001
                    if (bit)
                        crc ^= 0xA001;
                }
            }
            byte[] crcBytes = new byte[2];
            // using a bitwise AND operation (&) with the hexadecimal value 0xFF, which has a binary representation of 11111111. Performing this operation with any value effectively clears (sets to 0) all bits except the 8 least significant bits
            crcBytes[0] = (byte)(crc & 0xFF);
            crcBytes[1] = (byte)(crc >> 8);
            return crcBytes;
        }



    }
    #endregion
}
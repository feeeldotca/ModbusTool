using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    
    public class ByteArray
    {
        List<byte> list = null;
        // initialization
        public ByteArray()
        {
            list = new List<byte>();
        }
       // add a new byte item
        public void Add(byte item)
        {
            list.Add(item);
        }
        // add a new byte array
        public void Add(byte[] item)
        {
            list.AddRange(item);
        }
        // clear items in the byte array list
        public void Clear()
        {
            list.Clear();
        }

        public byte[] array
        {
            get { return list.ToArray(); }
        }
    }


}

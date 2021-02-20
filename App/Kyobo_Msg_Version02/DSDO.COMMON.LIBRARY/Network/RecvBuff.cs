using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.LIBRARY.Network
{
    public struct RecvBuff
    {
        //public const int BUFFER_SIZE = 8192*1024;
        public byte[] byteBuf;
        public int toRecv;
        public MemoryStream memoryStream;
        public int msgType; //0 String, 1 File
        //public string fileName;

        public RecvBuff(int toRec)
        {
            byteBuf = new byte[toRec];   // BUFFER_SIZE];
            toRecv = toRec;
            memoryStream = new MemoryStream(byteBuf, false); // new MemoryStream(toRec);
            msgType = 0;
            //fileName = "";
        }

        public void Dispose()
        {
            byteBuf = null;
            toRecv = 0;
            Close();
            if (memoryStream != null)
                memoryStream.Dispose();
        }

        void Close()
        {
            if (memoryStream != null && memoryStream.CanWrite)
                memoryStream.Close();
        }
    }
}

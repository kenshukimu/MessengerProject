using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.Network
{
    public class PacketResponse
    {
        public PacketID packetID { get; private set; }
        public string strBuf { get; private set; }

        public PacketResponse(PacketID opcode, string buffer)
        {
            this.packetID = opcode;
            this.strBuf = buffer;
        }

        public T Parsing<T>() where T : PacketDataBase
        {
            return JsonConvert.DeserializeObject<T>(this.strBuf);
        }
    }
}

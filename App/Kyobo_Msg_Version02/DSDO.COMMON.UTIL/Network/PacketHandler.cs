using DSDO.COMMON.UTIL.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.Network
{
    public abstract class PacketHandler
    {
        public PacketHandler() { }

        public bool HandlePacket(string sessionID, PacketResponse response)
        {
            return OnHandle(sessionID, response);
        }

        protected abstract bool OnHandle(string sessionID, PacketResponse response);
    }
   
    
}

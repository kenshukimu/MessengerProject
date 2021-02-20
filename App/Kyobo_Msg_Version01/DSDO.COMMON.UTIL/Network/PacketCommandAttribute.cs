using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.Network
{
    public class PacketCommandAttribute : Attribute
    {
        public PacketID packetID { get; set; }
        public PacketID waitPacketID { get; set; }

        public PacketCommandAttribute(PacketID _packetID, PacketID _waitPacketID = PacketID.PacketID_Init)
        {
            this.packetID = _packetID;
            this.waitPacketID = _waitPacketID;
        }

    }
}

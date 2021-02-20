using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.Network
{
    public class PacketHandlerList
    {
        private readonly Dictionary<int, PacketHandler> Handlers = null;
        private readonly Dictionary<PacketID, List<PacketID>> WaitProtocol = null;

        public PacketHandlerList(IEnumerable<PacketHandler> handlers)
        {
            this.Handlers = new Dictionary<int, PacketHandler>();
            this.WaitProtocol = new Dictionary<PacketID, List<PacketID>>();

            foreach (var h in handlers)
            {
                var attr = h.GetType().GetCustomAttributes(typeof(PacketCommandAttribute), true).FirstOrDefault() as PacketCommandAttribute;

                if (Handlers.ContainsKey((int)attr.packetID) == false)
                {
                    Handlers.Add((int)attr.packetID, h);
                }
                else
                {
                    Console.WriteLine("Already Protocol registed {0}", attr.packetID);
                }

                if (attr.waitPacketID != PacketID.PacketID_Init)
                {
                    List<PacketID> listOps;
                    if (WaitProtocol.TryGetValue(attr.waitPacketID, out listOps))
                    {
                        if (listOps.Contains(attr.packetID))
                            Console.WriteLine("already wait send op code . {0} {1}", attr.waitPacketID, attr.packetID);
                        else
                            listOps.Add(attr.packetID);
                    }
                    else
                    {
                        listOps = new List<PacketID>();
                        listOps.Add(attr.packetID);
                        WaitProtocol.Add(attr.waitPacketID, listOps);
                    }
                }
            }
        }

        public void Release()
        {
            Handlers.Clear();
            WaitProtocol.Clear();
        }

        public bool IsWaitProtocol(PacketID reqPacketID, out List<PacketID> recvpacketIDs)
        {
            if (WaitProtocol.TryGetValue(reqPacketID, out recvpacketIDs))
                return true;
            return false;
        }

        public bool HandleMessage(string sessionID, PacketResponse response)
        {
            if (false == Handlers.ContainsKey((int)response.packetID))
            {
                Console.WriteLine("Unknown Protocol: {0}", response.packetID);
                return false;
            }

            Handlers[(int)response.packetID].HandlePacket(sessionID, response);

            return true;
        }

    }
}
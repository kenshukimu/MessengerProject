using DSDO.COMMON.UTIL.Network;

namespace Kyobo_Msg_Server
{
    /*
     * DATE : 2020.08.10 
     * FORM_TITLE :패킷정보
     * AUTHOR : 교보정보통신 김현수
     * DESC : 패킷 기본 정보
     */

    [PacketCommand(PacketID.PacketID_Connect_Req, PacketID.PacketID_Connect_Ack)]
    public class PacketHandler_ConnectReq : PacketHandler
    {
        protected override bool OnHandle(string sessionID, PacketResponse response)
        {
            PacketData_ConnectReq res = response.Parsing<PacketData_ConnectReq>();
            //if(res.seatNo < 0)
            //{
            //    return false;
            //}

            //MainProgram.Instance.Login(res);
            return true;
        }
    }


}

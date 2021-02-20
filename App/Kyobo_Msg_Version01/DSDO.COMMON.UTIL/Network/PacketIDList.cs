namespace DSDO.COMMON.UTIL.Network
{
    public enum PacketID
    {
        PacketID_Init = 0,

        PacketID_Connect_Req = 100003,          //접속
        PacketID_Connect_Ack,
        PacketID_DisConnect_Req,

        //Client -> Server
        PacketID_Ping_Req = 909999,             //Ping 전송
        PacketID_Ping_Ack = PacketID_Ping_Req,
        PacketID_Send_Notice,
        PacketID_Send_Msg,             
        PacketID_Make_Room,
        PacketID_Close_Room,
        PacketID_Room_Msg,


        PacketID_Error,
        PacketID_ReloadGroup

    }

}

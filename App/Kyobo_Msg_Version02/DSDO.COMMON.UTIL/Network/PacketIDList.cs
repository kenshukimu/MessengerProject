namespace DSDO.COMMON.UTIL.Network
{
    public enum PacketID
    {
        PacketID_Init = 0,

        PacketID_Connect_Req = 100003,          //접속
        PacketID_Connect_Ack =100001,
        PacketID_DisConnect_Req =200001,

        //Client -> Server
        PacketID_Ping_Req = 909999,             //Ping 전송
        PacketID_Ping_Ack = PacketID_Ping_Req,
        PacketID_Send_Notice=3,
        PacketID_Send_Msg=4,             
        PacketID_Make_Room=5,
        PacketID_Close_Room=6,
        PacketID_Room_Msg=7,

        PacketID_Notice_Detail=8,
        PacketID_Notice_Find=9,

        PacketID_Message_Mng = 10,
        PacketID_Message_Detail = 11,
        PacketID_Message_Search = 12,
        PacketID_Message_ReWrite = 13,

        PacketID_Send_Msg_Read = 14,

        PacketID_Error,
        PacketID_ReloadGroup

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSDO.COMMON.UTIL.Network
{
    //class PacketData
    //{
    //}

    public abstract class PacketDataBase
    {
        public const int Success = 1;

        //public const int Err_WrongSeatNo = -9001;
        //public const string ErrMsg_WrongSeatNo = "잘못된 번호 입니다.\r\n자리 번호 변경 후 재 실행하여주세요.";
        //public const int Err_ExistSeatNo = -9002;
        //public const string ErrMsg_ExistSeatNo = "같은 번호 있어 충돌합니다.\r\n자리 번호 변경 후 재 실행하여주세요.";

        public const int Err_Unknown = -9999;
        public const string ErrMsg_Unknown = "알수없는 에러입니다.";
    }

    public abstract class PacketDataReq : PacketDataBase
    {
        public PacketID PacketID { get; set; }
        //public string strBuf { get; private set; }
    }

    public class PacketData_ConnectReq : PacketDataReq
    {
        public PacketData_ConnectReq()
        {
            PacketID = PacketID.PacketID_Connect_Req;
        }

        public String userId { get; set; }
        public List<String> presentSessionList { get; set; }
    }

    public class PacketData_DisConnectReq : PacketDataReq
    {
        public PacketData_DisConnectReq()
        {
            PacketID = PacketID.PacketID_DisConnect_Req;
        }

        public String userId { get; set; }
    }

    public class PacketData_Ping5Sec : PacketDataReq
    {
        public PacketData_Ping5Sec()
        {
            PacketID = PacketID.PacketID_Ping_Req;
        }

        public int nowTime { get; set; }        // 현재 시간
    }

    public class PacketData_SendNotice : PacketDataReq
    {
        public PacketData_SendNotice()
        {
            PacketID = PacketID.PacketID_Send_Notice;
        }

        public int boardNo { get; set; }
        public String userId { get; set; }
    }

    public class PacketData_SendMessage : PacketDataReq
    {
        public PacketData_SendMessage()
        {
            PacketID = PacketID.PacketID_Send_Msg;
        }

        public int boardNo { get; set; }
        public String userId { get; set; }
        public List<String> revUserId { get; set; }
    }

    public class PacketData_ConnectAck : PacketDataReq
    {
        public PacketData_ConnectAck()
        {
            PacketID = PacketID.PacketID_Connect_Ack;
        }

        public int result { get; set; }

        public String userId { get; set; }
    }

    public class PacketData_MakeRoom : PacketDataReq
    {
        public PacketData_MakeRoom()
        {
            PacketID = PacketID.PacketID_Make_Room;
        }

        public String roomNo { get; set; }
        public String makeUserId { get; set; }
        public List<String> revUserId { get; set; }
    }

    public class PacketData_CloseRoom : PacketDataReq
    {
        public PacketData_CloseRoom()
        {
            PacketID = PacketID.PacketID_Close_Room;
        }

        public String roomNo { get; set; }
        public String userId { get; set; }
    }

    public class PacketData_SendRoomMessage : PacketDataReq
    {
        public PacketData_SendRoomMessage()
        {
            PacketID = PacketID.PacketID_Room_Msg;
        }

        public String sendUserId { get; set; }
        public String message { get; set; }
        public String roomNo { get; set; }
    }

    public class PacketData_ReloadGroup : PacketDataReq
    {
        public PacketData_ReloadGroup()
        {
            PacketID = PacketID.PacketID_ReloadGroup;
        }
    }
}

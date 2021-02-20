using System;
using System.Collections;
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
        public IList<Hashtable> noticeList { get; set; }
        public IList<Hashtable> messageList { get; set; }
        public IList<Hashtable> userList { get; set; }
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

        public int noticeNum { get; set; }
        public String userId { get; set; }
        public Hashtable noticeDetail { get; set; }
        public IList<Hashtable> noticeList { get; set; }
        public IList<Hashtable> noticeDetail_Rev { get; set; }
        public String noticeProcess { get; set; }
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
        public Hashtable messageDetail { get; set; }
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

    public class PacketData_NoticeDetail : PacketDataReq
    {
        public PacketData_NoticeDetail()
        {
            PacketID = PacketID.PacketID_Notice_Detail;
        }

        public String noticeNum { get; set; }
        public String noticeProcess { get; set; }
        public IList<Hashtable> noticeDetail { get; set; }
    }

    public class PacketData_NoticeFind : PacketDataReq
    {
        public PacketData_NoticeFind()
        {
            PacketID = PacketID.PacketID_Notice_Find;
        }

        public Hashtable criFind { get; set; }
        public IList<Hashtable> noticeList { get; set; }
    }

    public class PacketData_MessageMng : PacketDataReq
    {
        public PacketData_MessageMng()
        {
            PacketID = PacketID.PacketID_Message_Mng;
        }

        public Hashtable criInfo { get; set; }
        public String process { get; set; }
        public IList<Hashtable> messageList { get; set; }     
    }

    public class PacketData_MessageDetail : PacketDataReq
    {
        public PacketData_MessageDetail()
        {
            PacketID = PacketID.PacketID_Message_Detail;
        }

        public Hashtable criInfo { get; set; }
        public IList<Hashtable> messageDetail { get; set; }
        public IList<Hashtable> messageReaderDetail { get; set; }

    }
    public class PacketData_MessageSearch : PacketDataReq
    {
        public PacketData_MessageSearch()
        {
            PacketID = PacketID.PacketID_Message_Search;
        }

        public Hashtable criInfo { get; set; }
        public IList<Hashtable> messageList { get; set; }
    }

    public class PacketData_MessageReWrite : PacketDataReq
    {
        public PacketData_MessageReWrite()
        {
            PacketID = PacketID.PacketID_Message_ReWrite;
        }

        public Hashtable criInfo { get; set; }
        public String rcvUser { get; set; }
        public IList<Hashtable> messageDetail { get; set; }
    }

    public class PacketData_SendMessage_Read : PacketDataReq
    {
        public PacketData_SendMessage_Read()
        {
            PacketID = PacketID.PacketID_Send_Msg_Read;
        }

        public Hashtable criInfo { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Kyobo_Msg_Server
{
    /*
     * DATE : 2020.08.10 
     * FORM_TITLE : 채팅방 정보
     * AUTHOR : 교보정보통신 김현수
     * DESC : 채팅방 기본 정보
     */
    class Room
    {
        public String roomNo { get; set; }
        public String mainUserID { get; set; }
        public List<Session> sessionList {get; set;}
    }
}

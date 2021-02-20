using KSM.DAO.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyobo_Msg_Server
{   
    public class DbRelation
    {
        _Sel _sel = new _Sel();
        _Ins _ins = new _Ins();

        public IList<Hashtable> getNoticeList()
        {
            Hashtable _param = new Hashtable();
            _param.Add("MESSAGE_KB", "0");
            IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderList");

            return _list;
        }

        public IList<Hashtable> getNoticeListFind(Hashtable _param)
        {
            IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderList");

            return _list;
        }

        public IList<Hashtable> getReadMessage(String userId)
        {
            Hashtable _param = new Hashtable();
            _param.Add("REGI_ID", userId);
            IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderDetail");
            return _list;
        }

        public IList<Hashtable> getUnReadBoarderDetail(String userId)
        {
            Hashtable _param = new Hashtable();
            _param.Add("REGI_ID", userId);
            _param.Add("MESSAGE_KB", "1");
            IList<Hashtable> _list = _sel.GetListHashData(_param, "getUnReadBoarderDetail");
            return _list;
        }

        public IList<Hashtable> getUserLIst()
        {
            IList<Hashtable> _list = new _Sel().GetListHashData(null, "getUserInfo");
            return _list;
        }

        public IList<Hashtable> getNoticeDetail(String noticeNum, String userId)
        {
            Hashtable _param = new Hashtable();
            _param.Add("C_INDEX", noticeNum);
            _param.Add("MESSAGE_KB", "0");

            _param.Add("ID_READER", userId);
            //읽음으로 표시
            _ins.UpdatetHashData(_param, "mergeNoticeMessageMsgDetail");

            IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderList");
            return _list;
        }

        public int insertNoticeDetail(Hashtable _param)
        {
            int boardNo = _ins.InsertHashDataBySelectKey(_param, "insertBoard");

            _param.Add("C_INDEX", boardNo);
            //자기자신은 읽음으로 표시
            _ins.UpdatetHashData(_param, "mergeNoticeMessageMsgDetail");

            return boardNo;
        }

        public void updateNoticeDetail(Hashtable _param)
        {
            _ins.UpdatetHashData(_param, "updateBoard");
        }

        public void deleteNoticeDetail(int _noticeNum)
        {
            Hashtable _param = new Hashtable();
            _param.Add("C_INDEX", _noticeNum);
            _ins.UpdatetHashData(_param, "delBoard");
        }

        public IList<Hashtable> getBoarderRcvList(Hashtable _param)
        {
            IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderRcvList");
            return _list;
        }

        public IList<Hashtable> getMessageDetail(Hashtable _param)
        { 
         
            IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderList");
            return _list;
        }

        public IList<Hashtable> getMessageReaderDetail(Hashtable _param)
        {
            IList<Hashtable> _list = _sel.GetListHashData(_param, "getBoarderDetail");
            return _list;
        }

        public void updateBoardDetail(Hashtable _param)
        {
            _ins.UpdatetHashData(_param, "updateBoardDetail");
        }

        public int insertMessageDetail(Hashtable _param, IList<String> rcvList, String sendUserId)
        {
            int boardNo = _ins.InsertHashDataBySelectKey(_param, "insertBoard");
            _param = new Hashtable();
            _param.Add("C_INDEX", boardNo);

            foreach (String userNm in rcvList)
            {
                if (userNm.Equals("")) continue;
                String _memberId = userNm;
                _param.Remove("ID_READER");
                //읽은 사람 등록
                _param.Add("ID_READER", _memberId);
                new _Ins().InsertHashData(_param, "insertBoardDetail");
            }
            return boardNo;
        }
    }
}


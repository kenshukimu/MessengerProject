using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using KSM.DAO.Models;

namespace Kyobo_Msg_Server
{
    [ServiceContract]
    public interface WcfIService
    {
        [OperationContract]
        Hashtable GetData(int value);

        // TODO: 여기에 서비스 작업을 추가합니다.
        [OperationContract]
        Hashtable LoginCheck(String userId, String password);
    }

    public class WcfService : WcfIService
    {
        public Hashtable GetData(int value)
        {
            Hashtable A = new Hashtable();
            A.Add("AAA", value);
            return A;
        }

        public Hashtable LoginCheck(String userId, String password)
        {
            Hashtable _param = new Hashtable();
            _param.Add("MGS_USERID", userId);
            _param.Add("PASSWD", password);

            IList<Hashtable> _list = new _Sel().GetListHashData(_param, "getLoginUserInfo");

            //IList<wcfUser> _rtnList = null;
            Hashtable _retHash = null;

            foreach(Hashtable _item in _list)
            {
                _retHash = new Hashtable();
                _retHash = _item;
            }
            return _retHash;
        }
    }

    //public class wcfUser
    //{
    //    public String m_MGS_USERID;
    //    public string m_SWCD;
    //    public string m_NAME;
    //    public string m_LEVEL_CD;
    //    public string m_ADMIN;
    //    public string m_ORG_CD;
    //    public string m_WRONGPASS;
    //    public string m_AUTH;
    //    public string m_GROUP_ID;
    //    public string m_PASSCHANGTIME;
    //    public string m_BADMIN;

    //    [DataMember]
    //    public string MGS_USERID
    //    {
    //        get { return m_MGS_USERID; }
    //        set { m_MGS_USERID = value; }
    //    }

    //    [DataMember]
    //    public string SWCD
    //    {
    //        get { return m_SWCD; }
    //        set { m_SWCD = value; }
    //    }

    //    [DataMember]
    //    public string NAME
    //    {
    //        get { return m_NAME; }
    //        set { m_NAME = value; }
    //    }

    //    [DataMember]
    //    public string LEVEL_CD
    //    {
    //        get { return m_LEVEL_CD; }
    //        set { m_LEVEL_CD = value; }
    //    }

    //    [DataMember]
    //    public string ADMIN
    //    {
    //        get { return m_ADMIN; }
    //        set { m_ADMIN = value; }
    //    }

    //    [DataMember]
    //    public string ORG_CD
    //    {
    //        get { return m_ORG_CD; }
    //        set { m_ORG_CD = value; }
    //    }
    //    [DataMember]
    //    public string WRONGPASS
    //    {
    //        get { return m_WRONGPASS; }
    //        set { m_WRONGPASS = value; }
    //    }
    //    [DataMember]
    //    public string PASSCHANGTIME
    //    {
    //        get { return m_PASSCHANGTIME; }
    //        set { m_PASSCHANGTIME = value; }
    //    }
    //    [DataMember]
    //    public string AUTH
    //    {
    //        get { return m_AUTH; }
    //        set { m_AUTH = value; }
    //    }
    //    [DataMember]
    //    public string GROUP_ID
    //    {
    //        get { return m_GROUP_ID; }
    //        set { m_GROUP_ID = value; }
    //    }
    //    [DataMember]
    //    public string BADMIN
    //    {
    //        get { return m_BADMIN; }
    //        set { m_BADMIN = value; }
    //    }
    //}
}

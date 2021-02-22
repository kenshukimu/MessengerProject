using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSM.DAO.Models
{
    public class UserDetailInfo : EntityBase
    {
        public String K_MEMBERID { get; set; }
        public string MEMBERID { get; set; }
        public string MEMBERNAME { get; set; }
        public string EMAIL { get; set; }
        public string OFFICEPHONE { get; set; }
        public string HP { get; set; }
        public string DESCRIPTION { get; set; }
        public string GROUPNAME { get; set; }
        public string RANKNAME { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSM.DAO.Models
{
    public class DaoParam
    {
        #region [■ LIKE 처리 Comment]
        /*
        private string kor_nm = string.Empty;
        private string birth = string.Empty;
        private string user_id = string.Empty;
        private string ipin_di = string.Empty;
               
        public string KOR_NM 
        {
            get { return kor_nm; }
            set { kor_nm = string.IsNullOrEmpty(value) ? value : value + "%" ; }
        }
        
        public string BIRTH
        {
            get { return birth; }
            set { birth = string.IsNullOrEmpty(value) ? value : value + "%"; }
        }

        public string USER_ID
        {
            get { return user_id; }
            set { user_id = string.IsNullOrEmpty(value) ? value : value + "%"; }
        }
        
        public string IPIN_DI
        {
            get { return ipin_di; }
            set { ipin_di = string.IsNullOrEmpty(value) ? value : value + "%"; }
        }
        */
        #endregion

        public string KOR_NM
        {
            get;
            set;
        }

        public string BIRTH
        {
            get;
            set;
        }

        public string USER_ID
        {
            get;
            set;
        }

        public string IPIN_DI
        {
            get;
            set;
        }

        public string PSN_NO { get; set; }

        public string EYEAR { get; set; }
        public string SERIES { get; set; }
        public string GCODE { get; set; }
        public string JMTYPE { get; set; }
        public string JMCD { get; set; }
        public string JMSCD { get; set; }
        public string DKCD { get; set; }
        public string EXAMTYPE { get; set; }
        public string SWCD { get; set; }
        public string PCODE { get; set; }
        public string JCODE { get; set; }
        public string CLASS { get; set; }
        public string EXAMNO { get; set; }
        public string PDATE { get; set; }
        
        public int START_ROW { get; set; }
        public int END_ROW { get; set; }
        public string START_DATE { get; set; }
        public string END_DATE { get; set; }
        public int ORDER_BY { get; set; }
        public string MGS_YN { get; set; }
        public string STATUS { get; set; }
        public string JMCD_DETAIL { get; set; }
        public string SUSIKIND { get; set; }
    }
}

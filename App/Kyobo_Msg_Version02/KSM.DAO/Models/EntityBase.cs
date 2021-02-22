using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSM.DAO.Models
{
    public abstract class EntityBase
    {
        private string crt_id = string.Empty;
		/// <summary>
		/// 등록일시
		/// </summary>
        public DateTime CRT_DT { get; set; }
		/// <summary>
		/// 등록자ID
		/// </summary>
        public string CRT_ID 
        {
            get { return crt_id; }
            set { crt_id = value != null ? value : string.Empty; }
        }
		/// <summary>
		/// 수정일시
		/// </summary>
		public DateTime UDT_DT { get; set; }
		/// <summary>
		/// 수정자ID
		/// </summary>
		public string UDT_ID { get; set; }

        public int START_ROW { get; set; }
        public int END_ROW { get; set; }

        public string START_DATE { get; set; }
        public string END_DATE { get; set; }
    }
}

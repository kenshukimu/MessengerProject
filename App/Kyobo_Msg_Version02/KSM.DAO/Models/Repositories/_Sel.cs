using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper;

namespace KSM.DAO.Models
{
    public class _Sel : DaoBase
    {
        public _Sel()
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
        { }

        /// <summary>
        /// 리스트 조회 후 해시태이블 반환
        /// <returns></returns>
        public IList<Hashtable> GetListHashData(Hashtable _param, String statement_param)
        {
            this.statement = statement_param;
            param.Clear();
            if (_param != null)
            {
                foreach (DictionaryEntry entry in _param)
                {
                    param.Add(entry.Key, entry.Value);
                }
            }

            IList<Hashtable> _list = null;

            try
            {
                LogDebug();
                _list = DaoFactory.Instance.QueryForList<Hashtable>(statement, param);
            }
            catch (Exception ex)
            {
                LogError(ex);    
                //throw ex;
            }

            return _list;
        }

        /// <summary>
        /// 리스트 조회 후 카운터 반환
        /// <returns></returns>
        public int GetListIntData(Hashtable _param, String statement_param)
        {
            this.statement = statement_param;
            param.Clear();
            if (_param != null)
            {
                foreach (DictionaryEntry entry in _param)
                {
                    param.Add(entry.Key, entry.Value);
                }
            }

            int cnt = 0;

            try
            {
                LogDebug();
                cnt = DaoFactory.Instance.QueryForObject<int>(statement, param);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }

            return cnt;
        }
    }
}

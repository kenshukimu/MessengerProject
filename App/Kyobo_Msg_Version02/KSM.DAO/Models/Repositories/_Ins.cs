using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using IBatisNet.DataMapper;

namespace KSM.DAO.Models
{
    public class _Ins : DaoBase
    {
        public _Ins()
            : base(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
        { }

        /// <summary>
        ///  SMS 개별송신처리
        /// </summary>
        /// <param name="_param"></param>
        /// <returns></returns>
        public void InsertTR_TRANData(Hashtable _param)
        {
            statement = "insertTR_TRANData";

            try
            {
                DaoFactory.Instance.Insert(statement, _param);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw ex;
            }
        }

        public void InsertMessengerAlert(Hashtable _param)
        {
            statement = "insertMessengerAlert";

            try
            {
                DaoFactory.Instance.Update(statement, _param);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw ex;
            }
        }

        public void InsertHashData(Hashtable _param, String _statment)
        {
            try
            {
                DaoFactory.Instance.Insert(_statment, _param);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw ex;
            }
        }

        public void DeleteHashData(Hashtable _param, String _statment)
        {
            try
            {
                DaoFactory.Instance.Delete(_statment, _param);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw ex;
            }
        }

        public int InsertHashDataBySelectKey(Hashtable _param, String _statment)
        {
            int boardNo = 0;
            try
            {
               boardNo = (int)DaoFactory.Instance.Insert(_statment, _param);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw ex;
            }
            return boardNo;
        }

        public int UpdatetHashData(Hashtable _param, String _statment)
        {
            int upd = 0;
            try
            {
                upd = DaoFactory.Instance.Update(_statment, _param);
            }
            catch (Exception ex)
            {
                LogError(ex);
                throw ex;
            }
            return upd;
        }
    }
}

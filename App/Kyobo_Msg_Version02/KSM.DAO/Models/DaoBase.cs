using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using log4net;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Scope;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.Common.Exceptions;
using System.Diagnostics;


namespace KSM.DAO.Models
{
	/// <summary>
	/// 
	/// </summary>
    public class DaoBase
    {
		/// <summary>
		/// 
		/// </summary>
        public DaoBase() : this(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType) { 
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
        public DaoBase(Type type) 
        {
            log = LogManager.GetLogger(type);
        }

		/// <summary>
		/// 
		/// </summary>
        protected ILog log = null;

		/// <summary>
		/// 파라미터
		/// </summary>
        protected Hashtable param = new Hashtable();

		/// <summary>
		/// 
		/// </summary>
        protected string statement { get; set; }

		/// <summary>
		/// SQL 문장을 지정한 오류 로그파일에 기록한다.
		/// </summary>
		/// <param name="ex"></param>
        protected void LogError(Exception ex) 
        {
            string strStmt = GetPreparedStatement(statement);
            using (log4net.NDC.Push(statement))
            {
                log.Error(string.Concat("[ ", strStmt, " ]"), ex);
            };
        }

		/// <summary>
		/// SQL 문장을 지정한 디버그 로그파일에 기록한다.
		/// </summary>
        protected void LogDebug()
        {
            string strStmt = GetPreparedStatement(statement);
            using (log4net.NDC.Push(statement))
            {
                log.Debug(string.Concat("[ ", strStmt, " ]"));
            };
        }

		/// <summary>
		/// SQL 문장을 출력창에 표시한다(디버깅 모드에서 표시).
		///		<para>트랜잭션 실행 중일 경우 아래 오류 발생</para>
		///		<para>SqlMap could not invoke BeginTransaction(). A Transaction is already started. Call CommitTransaction() or RollbackTransaction first.</para>
		/// </summary>
		/// <param name="showInOutputWindow">출력창 표시여부</param>
		protected void LogDebug(bool showInOutputWindow)
		{
			string strStmt = GetPreparedStatement(statement);
			if (showInOutputWindow)
			{
				Debug.WriteLine(strStmt);
			}
			else
			{
				using (log4net.NDC.Push(statement))
				{
					log.Debug(string.Concat("[ ", strStmt, " ]"));
				};
			}
		}

		/// <summary>
		/// SQL 문장을 가져온다.
		///		<para>트랜잭션 실행 중일 경우 아래 오류 발생</para>
		///		<para>SqlMap could not invoke BeginTransaction(). A Transaction is already started. Call CommitTransaction() or RollbackTransaction first.</para>
		/// </summary>
		/// <param name="statementName"></param>
		/// <returns></returns>
        protected String GetPreparedStatement(string statementName)
        {
            String strSQL = "";
			ISqlMapper mapper = DaoFactory.Instance;

			try
            {
                if (!mapper.IsSessionStarted) mapper.OpenConnection();	//트랜잭션 중간에 사용할 경우 오류 발생
                IMappedStatement statement = mapper.GetMappedStatement(statementName);
				RequestScope request = statement.Statement.Sql.GetRequestScope(statement, param, mapper.LocalSession);
                strSQL = request.PreparedStatement.PreparedSql;
                strSQL += "\r\n" + GetParameters(param);
				if (mapper.IsSessionStarted) mapper.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new IBatisNetException("Error Occured in GetPreparedStatement=" + statementName, ex);
            }

            return strSQL;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameterObject"></param>
		/// <returns></returns>
        protected String GetParameters(object parameterObject)
        {
            String result = " : ";
            if (parameterObject == null)
            {
                result += "null";
                return result;
            }

            if (parameterObject.GetType().ToString() == "System.Collections.Hashtable")
            {
                Hashtable dupTable = (Hashtable)parameterObject;
                foreach (String key in dupTable.Keys)
                {
                    Object resultValue = dupTable[key];
                    result = result + string.Format("{0}=[{1}], ", key, resultValue != null ? resultValue.ToString() : "null");
                }
            }
            if (result != "Parameters: ") result = result.Substring(0, result.Length - 2);
            return result;
        }

		/// <summary>
		/// 파라미터를 설정한다.
		/// </summary>
		/// <param name="_param"></param>
		protected void SetParameters(Hashtable _param)
		{
			param.Clear();

			foreach (DictionaryEntry entry in _param)
			{
				param.Add(entry.Key, entry.Value);
			}
		}

		/// <summary>
		/// 파라미터를 설정한다.
		/// </summary>
		/// <param name="_stamtement"></param>
		/// <param name="_param"></param>
		protected void SetParameters(string _stamtement, Hashtable _param)
		{
			statement = _stamtement;

			param.Clear();
			foreach (DictionaryEntry entry in _param)
			{
				param.Add(entry.Key, entry.Value);
			}
		}
    }
}

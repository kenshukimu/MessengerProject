package com.khs.common.handler;

import java.io.UnsupportedEncodingException;
import java.sql.SQLException;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.ibatis.sqlmap.client.extensions.ParameterSetter;
import com.ibatis.sqlmap.client.extensions.ResultGetter;
import com.ibatis.sqlmap.client.extensions.TypeHandlerCallback;

public class OracleUsTypeHandler  implements TypeHandlerCallback {

		private final Logger logger = LoggerFactory.getLogger(getClass());

		public Object getResult(ResultGetter getter) throws SQLException {
			String str = null;
			try {
//				System.out.println(" -------------getResult------------- ");
//				System.out.println("getter ::::: "+getter);
				
				str = new String(getter.getString().getBytes("8859_1"), "EUC_KR");
			} catch (UnsupportedEncodingException e) {
				this.logger.debug("UnsupportedEncodingException : "
						+ e.getMessage());
				str = getter.getString();
			} catch (Exception localException) {
			}
			return str;
		}

		public void setParameter(ParameterSetter setter, Object parameter)
				throws SQLException {
			String str = null;
			try {
//				System.out.println(" -------------setParameter------------- ");
				str = new String(((String) parameter).getBytes("KSC5601"), "8859_1");
			} catch (UnsupportedEncodingException e) {
				this.logger.debug("UnsupportedEncodingException : "
						+ e.getMessage());
				str = (String) parameter;
			} catch (Exception localException) {
			}
			setter.setString(str);
		}

		public Object valueOf(String s) {
			return s;
		}

	}
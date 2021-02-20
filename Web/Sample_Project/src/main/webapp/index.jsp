<!DOCTYPE html>
<%@ page language="java" contentType="text/html; charset=UTF-8" pageEncoding="UTF-8"%>
    
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="spring" uri="http://www.springframework.org/tags"%>

<%@ page import = "java.sql.Connection" %>
<%@ page import = "java.sql.DriverManager" %>

<%
boolean connection = false;
Connection conn = null;
String driver = "oracle.jdbc.driver.OracleDriver";
String url = "jdbc:oracle:thin:@localhost:1521:xe";
//String url = "jdbc:oracle:thin:@10.65.21.2:1521:KUM";

try {
	Class.forName(driver); 
	conn = DriverManager.getConnection(url, "KICO", "1234qwer!");
	connection = true;
	System.out.println("DB 연결 성공");
} catch (Exception e) {
	connection = false;
	System.out.println("DB 연결 실패");
	e.printStackTrace();
} 

%>

<html>
<head>
	<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1">
	<title>현수 놀이터</title>
</head>

<body class="example">
	<h1>KHS_PROJECT_SAMPLE_LIST</h1>
	<ul>
		<!-- <li><a href="demo/index.html">Example Browser</a>
		<li><a href="doc/jsdoc/index.html">API Documentation</a>
		<li><a href="https://github.com/mar10/fancytree">Project home</a> -->
		<li>
			<c:url value="/showMessage.do" var="messageUrl" />
			<a href="${messageUrl}">FTP CLIENT PROJECT</a>
		</li>	
		<li>
			<c:url value="/showUserList.do" var="messageUrl_2" />
			<a href="${messageUrl_2}">USERLIST PROJECT</a>
		</li>	
	</ul>
	<%if(connection == true) {%>
		연결되었습니다.
	<%} else {%>
		연결실패!!@
	<%}%>
	
</body>
</html>
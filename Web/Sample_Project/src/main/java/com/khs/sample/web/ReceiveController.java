package com.khs.sample.web;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.annotation.Resource;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.net.ftp.FTP;
import org.apache.commons.net.ftp.FTPClient;
import org.apache.commons.net.ftp.FTPFile;
import org.apache.commons.net.ftp.FTPReply;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import com.khs.sample.model.FTPDirInfo;
import com.khs.sample.model.FTPFileInfo;
import com.khs.sample.service.ReceiveService;

@Controller
public class ReceiveController {
	
	@Resource(name="ReceiveService")
	private ReceiveService receiveService;	
	
	//LinkedList<String> qu = new LinkedList<String>();
	//Stack<String> qu = new Stack<String>();
	ArrayList<String> qu = null;
	int _idx = 0;
	
	@RequestMapping("/showMessage.do")
	public String showMessage01(HttpServletRequest request , HttpServletResponse response, Model model) throws Exception {	
		try {
		   		
		} catch (Exception e) {
			e.printStackTrace();
		}
	
		return "showFtpList";
	}
	
	@SuppressWarnings("unchecked")
	@RequestMapping(value = "/getFileList.do", method = RequestMethod.POST, produces = "application/json")
	public @ResponseBody  HashMap<String,Object> getFileList(HttpServletRequest request) throws Exception {	
		HashMap<String,Object> result = new HashMap<String,Object>();
		
		try {
			
			//ftp전송
			String ip = "218.54.139.167";
			
			int port = 21;
			String id = "webFTP";
			String password = "111111";
			String rootPath = "_출제실";
				
			qu =  new ArrayList<String>();
			 _idx = 0;
			
			sendFtpServer(ip, port, id, password,"1");	
			
			List<FTPDirInfo> ftpDirList = new ArrayList<FTPDirInfo>();
			
			List<Map<String, Object>> treeList = new ArrayList<Map<String,Object>>();
				
        	for ( int i=0; i<qu.size(); i++ ) {        		
        		String[] root = qu.get(i).split("\\^");
        		String[] item=root[1].split("\\/");        		
        		//부모키
        		String pIdKey = "";
        		String sepKey = "";
        		
        		//부모키
        		for(int _len = 1; _len < item.length -1; _len++) {
        			pIdKey += "_" + item[_len];
        		}
        		
        		//디렉토리 설정키(중복방지)
        		for(int _len = 1; _len < item.length; _len++) {
        			sepKey += "_" + item[_len];
        		}
        		
        		if(pIdKey.length() < 1) {        			
        			FTPDirInfo fflf = new FTPDirInfo();			
        			fflf.setTitle(item[item.length-1]);
        			fflf.setExpanded(true);	
        			fflf.setKey(root[1]);
        			fflf.setChildren(new ArrayList<Object>());
        			            			
        			Map<String, Object> view = new HashMap<String, Object>();
        			view.put(rootPath, fflf);
        			
        			treeList.add(view);
        		}else{        			
    				for(Map<String, Object> _orgList : treeList) {
    					FTPDirInfo fdif =(FTPDirInfo)_orgList.get(pIdKey);
    					
    					if(fdif !=null) {
    						//디렉토리일경우
    						if(root[0].equals("D")) {
    							FTPDirInfo fflf = new FTPDirInfo();			
    	            			fflf.setTitle(item[item.length-1]);
    	            			fflf.setExpanded(true);	
    	            			fflf.setKey(root[1]);
    	            			fflf.setChildren(new ArrayList<Object>());
    	            			
    	            			((List<Object>)fdif.getChildren()).add(fflf);
    	            			            			
    	            			Map<String, Object> view = new HashMap<String, Object>();
    	            			view.put(sepKey, fflf);
    	            			
    	            			treeList.add(view);
    	            			break;
    						}else{ //파일일 경우
    							FTPFileInfo fflf = new FTPFileInfo();			
    	            			fflf.setTitle(item[item.length-1]);
    	            			fflf.setKey(root[1]);
    	            			
    	            			((List<Object>)fdif.getChildren()).add(fflf);
    	            			break;
    						}
    					}else{
    						continue;
    					}
    				}    				
        		}
	        }
        	
        	//root만 뽑아낸다.
        	Map<String, Object> rootData = treeList.get(0);
        	FTPDirInfo _fdi = (FTPDirInfo)rootData.get(rootPath);
        	
        	ftpDirList.add(_fdi);
			result.put("list", ftpDirList);
			result.put("msg", "OK");
			
		} catch (Exception e) {
			e.printStackTrace();
		}
	
		return result;
	}
	
	public boolean sendFtpServer(String ip, int port, String id, String password, String processKb) {
			boolean isSuccess = false;
			FTPClient ftp = null;
			int reply;
			try {
				
				ftp = new FTPClient();
				
				//ftp서버에 한글파일을 쓸때 한글깨짐 방지
				ftp.setControlEncoding("euc-kr");
				ftp.connect(ip, port);
				System.out.println("Connected to " + ip + " on "+ftp.getRemotePort());
				
				// After connection attempt, you should check the reply code to verify
				// success.
				reply = ftp.getReplyCode();
				if (!FTPReply.isPositiveCompletion(reply)) {
				  ftp.disconnect();
				  System.err.println("FTP server refused connection.");
				  System.exit(1);
				}
				
				if(!ftp.login(id, password)) {
				  ftp.logout();
				  throw new Exception("ftp 서버에 로그인하지 못했습니다.");
				}
				
				ftp.setFileType(FTP.BINARY_FILE_TYPE);
				ftp.enterLocalPassiveMode();
				
				System.out.println(ftp.printWorkingDirectory());
				
				if(processKb.equals("1")) {
					subDirList(ftp, "");
				}else{
					//fileDownLoad(ftp, null);
				}
				
				try{
				  
				}catch(Exception e){
				  e.printStackTrace();
				}			
							
				ftp.logout();
			} catch (Exception e) {
				e.printStackTrace();
			} finally {
			if (ftp != null && ftp.isConnected()) {
			  try { 
				  ftp.disconnect(); 
			  } catch (IOException e) {}
			}
		}
		return isSuccess;
	}
	
	public void subDirList(FTPClient ftp, String remotePath){		
		try{
			
			FTPFile[] files = null; 	
			ftp.changeWorkingDirectory(remotePath);			
			files = ftp.listFiles();
			
			for (FTPFile remoteFile : files)
		    {
		        if (!remoteFile.getName().equals(".") && !remoteFile.getName().equals(".."))
		        {
		            String remoteFilePath = remotePath + "/" + remoteFile.getName();
		            //String localFilePath = localPath + "/" + remoteFile.getName();

		            if (remoteFile.isDirectory())
		            {   
		            	//System.out.println("remote directory " + remoteFilePath);
		                qu.add(_idx, "D^" + remoteFilePath);		                
		                ++_idx;
		                subDirList(ftp, remoteFilePath);
		                //qu.offer("D^" + remoteFilePath);
		                
		            }
		            else
		            {
		                //System.out.println("remote file " + remoteFilePath);
		                //qu.offer("F^" + remoteFilePath);
		            	 qu.add(_idx, "F^" + remoteFilePath);			                
			             ++_idx;
		                
		                /*OutputStream outputStream =
		                    new BufferedOutputStream(new FileOutputStream(localFilePath));
		                if (!ftp.retrieveFile(remoteFilePath, outputStream))
		                {
		                    System.out.println("Failed to download file " + remoteFilePath);
		                }
		                outputStream.close();*/
		            }
		        }
		    }
		}catch(IOException e){
			System.out.println("서버로 부터 디렉토리를 가져오지 못했습니다.");

		}
	}	
	
	/*
	public void fileDownLoad(FTPClient ftp, String[] remotePath){		
		String root = "d:\\temp\\ftp";

		try{
			ftp.changeWorkingDirectory("/출제실/1.전산회계운용사/");			
			//files = ftp.listFiles();
			System.out.println(ftp.printWorkingDirectory());
			remotePath = new String[1];
			remotePath[0] = "- 전산회계운용사 자격 출제기준 현행-개편(안) 비교표.hwp";
			for (String remoteFile : remotePath)
		    {
				try (FileOutputStream fo = new FileOutputStream(root + File.separator + remoteFile)) {
					// FTPClient의 retrieveFile함수로 보내면 다운로드가 이루어 진다.
					if (ftp.retrieveFile(remoteFile, fo)) {
						System.out.println("Download - " + remoteFile);
					}
				}
		    }
		}catch(IOException e){
			System.out.println("서버로 부터 파일을 가져오지 못했습니다.");

		}
	}	*/
}

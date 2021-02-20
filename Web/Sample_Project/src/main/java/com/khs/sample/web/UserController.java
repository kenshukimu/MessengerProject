package com.khs.sample.web;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.annotation.Resource;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import com.khs.sample.model.UserDepInfo;
import com.khs.sample.model.UserInfo;
import com.khs.sample.model.kcciUser;
import com.khs.sample.service.UserService;

@Controller
public class UserController {
	
	@Resource(name="UserService")
	private UserService userService;	
	
	ArrayList<String> qu = null;
	int _idx = 0;
	
	@RequestMapping("/showUserList.do")
	public String showMessage01(HttpServletRequest request , HttpServletResponse response, Model model) throws Exception {	
		try {
		   		
		} catch (Exception e) {
			e.printStackTrace();
		}
	
		return "showUserList";
	}
	
	@SuppressWarnings("unchecked")
	@RequestMapping(value = "/getUserList.do", method = RequestMethod.POST, produces = "application/json")
	public @ResponseBody  HashMap<String,Object> getUserList(HttpServletRequest request) throws Exception {	
		HashMap<String,Object> result = new HashMap<String,Object>();
		
		String rootPath = "_전국상공회의소";
		qu =  new ArrayList<String>();
		 _idx = 0;
		
		try {	
			List<UserDepInfo> userDeptList = new ArrayList<UserDepInfo>();
			
			List<Map<String, Object>> treeList = new ArrayList<Map<String,Object>>();
			getUserList();
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
        		
        		String tilte = "";
        		
        		tilte += item[item.length-1];
        		if(root[0].equals("F")) {
        			tilte += " " + root[3];
        		}
        		
        		if(pIdKey.length() < 1) {        			
        			UserDepInfo fflf = new UserDepInfo();			
        			fflf.setTitle(tilte);
        			fflf.setExpanded(true);	
        			fflf.setKey(root[2]);
        			fflf.setType(root[0]);
        			
        			fflf.setChildren(new ArrayList<Object>());
        			            			
        			Map<String, Object> view = new HashMap<String, Object>();
        			view.put(rootPath, fflf);
        			
        			treeList.add(view);
        		}else{        			
    				for(Map<String, Object> _orgList : treeList) {
    					UserDepInfo fdif =(UserDepInfo)_orgList.get(pIdKey);
    					
    					if(fdif !=null) {
    						//디렉토리일경우
    						if(root[0].equals("D")) {
    							UserDepInfo fflf = new UserDepInfo();			
    	            			fflf.setTitle(tilte);
    	            			fflf.setExpanded(false);	
    	            			fflf.setKey(root[2]);
    	            			fflf.setType(root[0]);
    	            			fflf.setChildren(new ArrayList<Object>());
    	            			
    	            			((List<Object>)fdif.getChildren()).add(fflf);
    	            			            			
    	            			Map<String, Object> view = new HashMap<String, Object>();
    	            			view.put(sepKey, fflf);
    	            			
    	            			treeList.add(view);
    	            			break;
    						}else{ //파일일 경우
    							UserInfo fflf = new UserInfo();			
    	            			fflf.setTitle(tilte);
    	            			fflf.setKey(root[2]);
    	            			fflf.setType(root[0]);
    	            			
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
        	UserDepInfo _fdi = (UserDepInfo)rootData.get(rootPath);
        	
        	userDeptList.add(_fdi);
			result.put("list", userDeptList);
			result.put("msg", "OK");
			
		} catch (Exception e) {
			e.printStackTrace();
		}
	
		return result;
	}
	
	public void getUserList(){		
		try{			
			List<kcciUser> _kcciUsers = userService.selectKcciUser();
			
			if(_kcciUsers !=null && _kcciUsers.size() > 0) {
				for (kcciUser item : _kcciUsers) {
					if(item.getK_memberid().equals("0") ) {
						  qu.add(_idx, "D^" + item.getPath() + "^" + "D" + item.getKey()  + "^" + item.getNickName());		                
			              ++_idx;
					}else{
						 qu.add(_idx, "F^" + item.getPath()+ "^" + "F" + item.getKey() + "^" + item.getNickName());			                
			             ++_idx;
					}
				}
			}			
		}catch(Exception e){
			System.out.println(e.getMessage());
			System.out.println("유저리스트를 가져오지 못했습니다.");
		}
	}		
}

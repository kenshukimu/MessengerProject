/*
 * Copyright 2008-2009 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
package com.khs.sample.service.impl;

import java.util.List;

import javax.annotation.Resource;

import org.springframework.stereotype.Service;

import com.khs.sample.dao.TestUserDAO;
import com.khs.sample.model.kcciUser;
import com.khs.sample.service.UserService;

/**
 *  ReceiveServiceImpl
 * 
 * @author user
 * @since 2015.11.01
 * @version 
 * @see <pre>
 *  == 개정이력(Modification Information) ==
 *   
 *   수정일			수정자				수정내용
 *  ---------------------------------------------------------------------------------
 *   2015.11.01	      user	                    최초생성
 * 
 * </pre>
 */

@Service("UserService")
public class UserServiceImpl implements UserService {

//	private static final Logger LOGGER = LoggerFactory.getLogger(GovFrameRestServiceImpl.class);

	/** TestUserDAO */
	@Resource(name="TestUserDAO")
	private TestUserDAO testUserDAO;


	public List<kcciUser> selectKcciUser() throws Exception {
		return testUserDAO.selectKcciUser();
	}
}

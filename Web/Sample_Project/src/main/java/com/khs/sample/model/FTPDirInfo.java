package com.khs.sample.model;

import java.util.List;

public class FTPDirInfo  {
	
	private List<?> children;
	private boolean expanded;
	private String title;
	private String key;
	
	public List<?> getChildren() {
		return children;
	}
	public void setChildren(List<?> children) {
		this.children = children;
	}
	public boolean isExpanded() {
		return expanded;
	}
	public void setExpanded(boolean expanded) {
		this.expanded = expanded;
	}
	
	public String getTitle() {
		return title;
	}
	public void setTitle(String title) {
		this.title = title;
	}
	public String getKey() {
		return key;
	}
	public void setKey(String key) {
		this.key = key;
	}
}

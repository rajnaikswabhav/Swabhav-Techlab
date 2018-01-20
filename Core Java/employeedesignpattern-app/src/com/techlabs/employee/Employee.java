package com.techlabs.employee;

import java.util.ArrayList;
import java.util.List;

public class Employee {
	private String id;
	private String name;
	private String managerID;
	private List<Employee> reporteeList;

	public Employee(String id, String name,String managerID) {
		this.id = id;
		this.name = name;
		this.managerID = managerID;
		reporteeList = new ArrayList<Employee>() ;
	}

	public String getId() {
		return id;
	}

	public String getName() {
		return name;
	}

	public String getManagerID() {
		return managerID;
	}
	
	

	public List<Employee> getReporteeList() {
		return reporteeList;
	}

	public void setReporteeList(List<Employee> reporteeList) {
		this.reporteeList = reporteeList;
	}
	
	public void addReportee(Employee employee){
		reporteeList.add(employee);
	}

	@Override
	public String toString() {
		return  getName();
	}

}

package com.techlabs.employee;

import java.util.ArrayList;
import java.util.List;

public class Employee {
	private String employeeId;
	private String employeeName;
	private String managerID;
	private List<Employee> reporteeList;

	public Employee(String employeeId, String employeeName, String managerID) {
		this.employeeId = employeeId;
		this.employeeName = employeeName;
		this.managerID = managerID;
		reporteeList = new ArrayList<Employee>();
	}

	public String getId() {
		return employeeId;
	}

	public String getName() {
		return employeeName;
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

	public void addReportee(Employee employee) {
		reporteeList.add(employee);
	}

	@Override
	public String toString() {
		return getName();
	}

}

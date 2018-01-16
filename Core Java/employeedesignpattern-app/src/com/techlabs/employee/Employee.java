package com.techlabs.employee;

import java.util.ArrayList;
import java.util.List;

public class Employee {
	private String id;
	private String name;
	private String designation;
	private String managerID;
	private String joiningDate;
	private String dearnessAllownce;
	private String departments;
	private List<Employee> reporteeList = new ArrayList<Employee>();

	public Employee(String id, String name, String designstion,
			String managerID, String birthDate, String dearnessAllowance,
			String workingDays) {
		this.id = id;
		this.name = name;
		this.managerID = managerID;
		this.designation = designstion;
		this.joiningDate = birthDate;
		this.dearnessAllownce = dearnessAllowance;
		this.departments = workingDays;
	}

	public String getId() {
		return id;
	}

	public String getName() {
		return name;
	}

	public String getDesignation() {
		return designation;
	}

	public String getManagerID() {
		return managerID;
	}

	public String getBirthDate() {
		return joiningDate;
	}

	public String getDearnessAllownce() {
		return dearnessAllownce;
	}

	public String getWorkingDays() {
		return departments;
	}

	@Override
	public String toString() {
		return getId() + "," + getName() + "," + getDesignation() + ","
				+ getManagerID() + "," + getBirthDate() + ","
				+ getDearnessAllownce() + "," + getWorkingDays();
	}

}

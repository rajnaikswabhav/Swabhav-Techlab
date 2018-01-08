package com.techlabs.employee;


public class Employee {

	private String id;
	private String name;
	private String designation;
	private String salary;
	private String birthDate;
	private String dearnessAllownce;
	private String workingDays;
	
	public Employee(String id,String name,String designstion,String salary,String birthDate,String dearnessAllowance,String workingDays){
		this.id=id;
		this.name=name;
		this.salary=salary;
		this.designation=designstion;
		this.birthDate=birthDate;
		this.dearnessAllownce=dearnessAllowance;
		this.workingDays=workingDays;
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

	public String getSalary() {
		return salary;
	}

	public String getBirthDate() {
		return birthDate;
	}

	public String getDearnessAllownce() {
		return dearnessAllownce;
	}

	public String getWorkingDays() {
		return workingDays;
	}
	
	
}

package com.techlabs.employee;

import java.time.LocalDate;
import java.util.Date;


public abstract class Employee {

	private String employeeName;
	private Date dateOfBirth;
	private double basicSalary;

	public Employee(String employeName, Date dateOfBirth, double basicSalary) {
		this.employeeName = employeName;
		this.dateOfBirth = dateOfBirth;
		this.basicSalary = basicSalary;
	}

	public String getEmployeeName() {
		return employeeName;
	}

	public Date getDateOfBirth() {
		return dateOfBirth;
	}

	public double getBasicSalary() {
		return basicSalary;
	}

	public float calculateAge() {
		float dob=0;
		long millis=System.currentTimeMillis();  
		Date currentDate=new Date();
		System.out.println(currentDate.getYear());
		return dob;
	}

	public abstract double calculateNetSalary();
}
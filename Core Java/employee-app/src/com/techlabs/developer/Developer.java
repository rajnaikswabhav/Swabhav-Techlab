package com.techlabs.developer;



import java.util.Date;

import com.techlabs.employee.Employee;

public class Developer extends Employee {

	private double performance;

	public Developer(String employeName, Date dateOfBirth, double basicSalary) {
		super(employeName, dateOfBirth, basicSalary);
	}

	public double getPerformance() {
		performance=getBasicSalary()*20/100;
		return performance;
	}

	@Override
	public double calculateNetSalary() {
		return getBasicSalary()+getPerformance();
	}

}

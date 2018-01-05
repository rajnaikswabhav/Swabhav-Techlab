package com.techlabs.manager;




import java.util.Date;

import com.techlabs.employee.Employee;

public class Manager extends Employee {

	private double HouseRentAllowance;
	private double DernessAllowance;

	public Manager(String employeName, Date dateOfBirth, double basicSalary) {
		super(employeName, dateOfBirth, basicSalary);
	}

	public double getHouseRentAllowance() {
		HouseRentAllowance=(getBasicSalary()*50)/100;
		return HouseRentAllowance;
	}

	public double getDernessAllowance() {
		DernessAllowance=(getBasicSalary()*40)/100;
		return DernessAllowance;
	}

	@Override
	public double calculateNetSalary() {
		double salary=getBasicSalary() + getHouseRentAllowance()
				+ getDernessAllowance();

		return salary;	
		}

}

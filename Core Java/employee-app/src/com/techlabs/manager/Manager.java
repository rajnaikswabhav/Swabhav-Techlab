package com.techlabs.manager;

import java.util.Date;

import com.techlabs.employee.Employee;

public class Manager extends Employee {

	private double houseRentAllowance;
	private double dernessAllowance;

	public Manager(String employeName, Date dateOfBirth, double basicSalary) {
		super(employeName, dateOfBirth, basicSalary);
	}

	public double getHouseRentAllowance() {
		houseRentAllowance = (getBasicSalary() * 50) / 100;
		return houseRentAllowance;
	}

	public double getDernessAllowance() {
		dernessAllowance = (getBasicSalary() * 40) / 100;
		return dernessAllowance;
	}

	@Override
	public double calculateNetSalary() {
		double salary = getBasicSalary() + getHouseRentAllowance()
				+ getDernessAllowance();

		return salary;
	}
}

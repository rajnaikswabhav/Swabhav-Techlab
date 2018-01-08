package com.techlabs.employee;

import java.util.Calendar;
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
		float dob = 0;
		Calendar birthDate = Calendar.getInstance();
		birthDate.setTime(getDateOfBirth());
		Calendar today = Calendar.getInstance();
		float curYear = today.get(Calendar.YEAR);
		float birYear = birthDate.get(Calendar.YEAR);
		dob = curYear - birYear;
		float curMonth = today.get(Calendar.MONTH);

		float birMonth = birthDate.get(Calendar.MONTH);

		if (birMonth > curMonth) { // this year can't be counted!

			dob--;

		} else if (birMonth == curMonth) { // same month? check for day

			int curDay = today.get(Calendar.DAY_OF_MONTH);

			int dobDay = birthDate.get(Calendar.DAY_OF_MONTH);

			if (dobDay > curDay) { // this year can't be counted!

				dob--;

			}

		}

		return dob;
	}

	public abstract double calculateNetSalary();
}
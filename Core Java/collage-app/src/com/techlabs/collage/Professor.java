package com.techlabs.collage;

import com.techlabs.interfaces.ISalarayCalulator;

public class Professor extends Person implements ISalarayCalulator {

	private double salary;

	public Professor(int id, String address, String dob) {
		super(id, address, dob);
	}

	public double getSalary() {
		return salary;
	}

	@Override
	public String toString() {
		
		String details = "Professor Details.......\n";
		details += "Id : "+getId()+ "\n";
		details += "Address : " +getAddress()+ "\n";
		details += "DOB : " +getDob()+ "\n";
		details += "Salary : " +getSalary()+ "\n";
		return details ;
	}

	@Override
	public double calculateSalary(int days) {

		double oneDaySalary = 1000;
		salary = days * oneDaySalary;
		return salary;
	}

}

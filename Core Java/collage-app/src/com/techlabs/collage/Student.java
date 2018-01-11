package com.techlabs.collage;

import com.techlabs.enums.Branch;

public class Student extends Person {

	private Branch branch;

	public Student(int id, String address, String dob, Branch branch) {
		super(id, address, dob);
		this.branch = branch;
	}

	public Branch getBranch() {
		return branch;
	}

	@Override
	public String toString() {
		String details = "Student Details.......\n";
		details += "Id : "+getId()+ "\n";
		details += "Address : " +getAddress()+ "\n";
		details += "DOB : " +getDob()+ "\n";
		details += "Branch : " +getBranch()+ "\n";
		return details ;
	}
}

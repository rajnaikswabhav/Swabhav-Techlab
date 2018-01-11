package com.techlabs.collage;

public class Person {

	private int id;
	private String address;
	private String dob;
	
	public Person(int id,String address,String dob){
		this.id=id;
		this.address=address;
		this.dob=dob;
	}

	public int getId() {
		return id;
	}

	public String getAddress() {
		return address;
	}

	public String getDob() {
		return dob;
	}
	
	
}

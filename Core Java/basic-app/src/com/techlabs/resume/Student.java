package com.techlabs.resume;

import java.io.BufferedWriter;
import java.io.FileWriter;

public class Student {
	private String name;
	private String address;
	private String phoneNo;
	private String occupication;
	private String email;

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getAddress() {
		return address;
	}

	public void setAddress(String address) {
		this.address = address;
	}

	public String getPhoneNo() {
		return phoneNo;
	}

	public void setPhoneNo(String phoneNo) {
		this.phoneNo = phoneNo;
	}

	public String getOccupication() {
		return occupication;
	}

	public void setOccupication(String occupication) {
		this.occupication = occupication;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public void buildResume() {
		String resume = "<html> <head><title>" + getName()
				+ " -Resume </title></head>";
		resume += "<body>" + "<h1 align=center> Resume </h1>";
		resume += "<h2> Name :" + getName() + "</h2>";
		resume += "<h2> Address :" + getAddress() + "</h2>";
		resume += "<h2> Phone No :" + getPhoneNo() + "</h2>";
		resume += "<h2> Occupication :" + getOccupication() + "</h2>";
		resume += "<h2> Email :" + getEmail() + "</h2>";
		try {
			FileWriter writer = new FileWriter(
					"src/com/techlabs/exercise/Data/Resume.html");
			BufferedWriter bufferedWriter = new BufferedWriter(writer);
			do {
				bufferedWriter.write(resume);
				bufferedWriter.newLine();
			} while (resume.equals(""));

			bufferedWriter.close();
		} catch (Exception e) {
			System.out.println("" + e.toString());
		}
	}
}

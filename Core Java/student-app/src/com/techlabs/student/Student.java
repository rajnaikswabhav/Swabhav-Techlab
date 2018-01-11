package com.techlabs.student;

import java.io.Serializable;
import java.text.SimpleDateFormat;
import java.util.Date;

public class Student implements Serializable {

	private static final long serialVersionUID = 1L;
	private final String firstName;
	private final String lastName;
	private final String address;
	private final long id;

	public Student(String firstName, String lastName, String address) {
		this.firstName = firstName;
		this.lastName = lastName;
		this.address = address;
		this.id = Long.parseLong(new SimpleDateFormat("yyyyMMddHHmmss")
				.format(new Date()));
	}

	public Student(String firstName, String lastName, String address, long id) {
		this.firstName = firstName;
		this.id = id;
		this.lastName = lastName;
		this.address = address;
	}

	public long getId() {
		return id;
	}

	public String getFirstName() {
		return firstName;
	}

	public String getLastName() {
		return lastName;
	}

	public String getAddress() {
		return address;
	}
}

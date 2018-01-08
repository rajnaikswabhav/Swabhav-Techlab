package com.techlabs.student;

public class Student {

	private int rollNo;
	private int standard;
	private String name;
	
	public Student(int rollNo,int standard,String name)
	{
		this.rollNo=rollNo;
		this.standard=standard;
		this.name=name;
	}

	public int getRollNo() {
		return rollNo;
	}

	public int getStandard() {
		return standard;
	}

	public String getName() {
		return name;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + rollNo;
		result = prime * result + standard;
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Student other = (Student) obj;
		if (rollNo != other.rollNo)
			return false;
		if (standard != other.standard)
			return false;
		return true;
	}
}

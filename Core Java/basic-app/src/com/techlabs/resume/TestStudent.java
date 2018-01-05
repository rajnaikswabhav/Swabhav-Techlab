package com.techlabs.resume;

public class TestStudent {

	public static void main(String[] args) {
		Student student = new Student();
		student.setName("Akash Malaviya");
		student.setPhoneNo("7405679727");
		student.setAddress("Ahmedabad,Gujarat");
		student.setOccupication("Software Engineer");
		student.setEmail("akashmalaviya101@gmail.com");
		student.buildResume();
	}

}

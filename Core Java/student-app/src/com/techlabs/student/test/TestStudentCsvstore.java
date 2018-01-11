package com.techlabs.student.test;

import java.util.ArrayList;

import com.techlabs.student.Student;
import com.techlabs.student.StudentCsvStore;

public class TestStudentCsvstore {

	public static void main(String[] args) {
		StudentCsvStore studentcsv = new StudentCsvStore();
		studentcsv.add(new Student("Akash","Malaviya","Ahmedabad"));
		print(studentcsv.get());
	}
	
	private static void print(ArrayList<Student> students) {
		for (Student student : students) {
			System.out.println("Student Id:" + student.getId());
			System.out.println("Student Name:" + student.getFirstName()+ " " +student.getLastName());
			System.out.println("Student Address:" + student.getAddress()
					+ "\n");
		}
	}
}

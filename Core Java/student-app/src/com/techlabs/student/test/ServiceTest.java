package com.techlabs.student.test;

import java.util.ArrayList;

import com.techlabs.student.Student;
import com.techlabs.student.StudentService;

public class ServiceTest {

	public static void main(String[] args) {
		Student student = new Student();
		StudentService studentService = new StudentService();
		student.setFirstName("Devang");
		student.setLastName("Vaghela");
		student.setAddress("Ahmedbad");
		studentService.addStudent(student);

		Student student2 = new Student();
		student2.setFirstName("Mahavir");
		student2.setLastName("Kasela");
		student2.setAddress("Mumbai");
		studentService.addStudent(student2);

		ArrayList<Student> listStudents = new ArrayList<Student>();
		listStudents = studentService.get();
		for (Student s : listStudents) {
			System.out.println("StudentName : " + s.getFirstName() + " "
					+ s.getLastName() + " \nAddress : " + s.getAddress() + "");
		}

	
		 // System.out.println("Search Method"); studentService.Search("mahavir");
		 
	}

}

package com.techlabs.student;

import java.util.ArrayList;
import java.util.Scanner;

public class StudentConsole {

	public static int ADD_CHOICE = 1;
	public static int RETRIVE_CHOICE = 2;
	int choice = 0;
	public static Scanner scanner;
	public void Start() {
		StudentService studentService= new StudentService();
		scanner = new Scanner(System.in);
		System.out.println("For Add Student Details Press 1:");
		System.out.println("For Display All Students Details Press 2:");
		System.out.println("Any Other Number for Exit:");

		choice = Integer.parseInt(scanner.nextLine());
		if (choice == ADD_CHOICE) {
			studentDetail();
		} else if (choice == RETRIVE_CHOICE) {
			studentService.get();
			Start();
		} else {
			System.exit(0);
		}
	}

	public void studentDetail() {
		Student student = new Student();
		StudentService studentService= new StudentService();
		// listOfStudents = new ArrayList<Student>();
		// do{
		System.out.print("Enter Student's First Name: ");
		String fname = scanner.nextLine();
		student.setFirstName(fname);

		System.out.print("Enter Student's Last Name: ");
		String lname = scanner.nextLine();
		student.setLastName(lname);

		System.out.print("Enater Student's Address: ");
		String address = scanner.nextLine();
		student.setAddress(address);

		// listOfStudents.add(student);

		System.out.println("Your Data Successfully Saved:");
		System.out.println();
		studentService.addStudent(student);
		// studentService.addStudent(listOfStudents);
		Start();
		// }while(Integer.parseInt(scanner.nextLine()) == 3);
	}
}

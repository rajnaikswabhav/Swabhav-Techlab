package com.techlabs.student;

import java.util.ArrayList;
import java.util.Scanner;

import com.techlabs.studentstore.IStudentStore;

public class StudentConsole {
	private IStudentStore studentobj;

	private static Scanner input;
	private static final int AddChoice = 1;
	private static final int DisplayChoice = 2;
	private static final int SearchStudentChoice = 3;
	private static final int DeleteStudentChoice = 4;
	private static final int ExportToHTML = 5;

	public StudentConsole(IStudentStore obj) {
		studentobj = obj;
	}

	public void start() {

		input = new Scanner(System.in);
		System.out.println("Press 1 to add Student");
		System.out.println("Press 2 to Display Students");
		System.out.println("Press 3 to Search Student By Name");
		System.out.println("Press 4 to Delete Student");
		System.out.println("Press 5 to Export to HTML File");

		int choice = Integer.parseInt(input.nextLine());

		switch (choice) {
		case AddChoice:
			getDetails();
			break;
		case DisplayChoice:
			print(studentobj.get());
			break;
		case SearchStudentChoice:
			getSearch();
			break;
		case DeleteStudentChoice:
			getDelete();
			studentobj.delete(0);
			break;
		case ExportToHTML:
			studentobj.export();
			System.out.println("Data has been exported to CSV file");
			break;
		}

	}

		public void getDetails() {

		do {
			System.out.println("Enter Student FirstName :");
			String firstName = input.nextLine();

			System.out.println("Enter Student LastName :");
			String lastName = input.nextLine();

			System.out.println("Enter Student Address :");
			String address = input.nextLine();

			Student student = new Student(firstName, lastName, address);
			((IStudentStore) studentobj).add(student);

			System.out.println("Thank You !! Your details are being saved.");
			System.out.println("Press 1 to add more Student");
			System.out.println("Any Other number to exit");

		} while (Integer.parseInt(input.nextLine()) == AddChoice);
		start();
	}

	public void print(ArrayList<Student> students) {
		for (Student student : students) {
			System.out.println("Student Id:" + student.getId());
			System.out.println("Student Name:" + student.getFirstName() + " "
					+ student.getLastName());
			System.out.println("Student Location:" + student.getAddress()
					+ "\n");
		}
	}

	public void getSearch() {
		System.out.println("Enter Student Name :");
		Student result = studentobj.search(input.nextLine());

		System.out.println("Student ID:" + result.getId());
		System.out.println("Student Name:" + result.getFirstName()
				+ result.getLastName());
		System.out.println("Student Location:" + result.getAddress());
	}

	private void getDelete() {
		System.out.println("Enter Student Id to delete:");
		System.out.println(studentobj.delete(Long.parseLong(input.nextLine())));
	}
}

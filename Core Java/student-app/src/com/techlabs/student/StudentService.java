package com.techlabs.student;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.ArrayList;

public class StudentService {
	public static ArrayList<Student> listStudent;

	public void addStudent(Student student) {
		ArrayList<Student> listofStudent = new ArrayList<Student>();
		listofStudent.add(student);
		save(listofStudent);

		/*
		 * try { FileOutputStream fileOutputStream = new FileOutputStream(
		 * "Data/StudentDetails.ser", true); ObjectOutputStream
		 * objectOutputStream = new ObjectOutputStream( fileOutputStream);
		 * objectOutputStream.writeObject(listStudent);
		 * objectOutputStream.close(); } catch (Exception e) {
		 * System.out.println("" + e.toString()); }
		 */
	}

	public void save(ArrayList<Student> listStudents) {
		try {
			FileOutputStream fileOutputStream = new FileOutputStream(
					"Data/StudentDetails.ser",true);
			ObjectOutputStream objectOutputStream = new ObjectOutputStream(
					fileOutputStream);
			objectOutputStream.writeObject(listStudents);
			objectOutputStream.close();
		} catch (Exception e) {
			System.out.println("" + e.toString());
		}
	}

	@SuppressWarnings("unchecked")
	public void get() {
		listStudent = new ArrayList<Student>();
		try {
			FileInputStream fileInputStream = new FileInputStream(
					"Data/StudentDetails.ser");
			ObjectInputStream objectInputStream = new ObjectInputStream(
					fileInputStream);
			listStudent = (ArrayList<Student>) objectInputStream.readObject();

			for (Student s : listStudent) {
				String result="Student Name: " +s.getFirstName() + " "
						+ s.getLastName() + "\nAddress:" + s.getAddress() + "\n";
				System.out.println(result);
			}
			objectInputStream.close();
		} catch (Exception e) {
			System.out.println("error:" + e.getMessage());
		}
	}
}

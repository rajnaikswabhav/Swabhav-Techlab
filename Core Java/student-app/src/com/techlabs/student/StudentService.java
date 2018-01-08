package com.techlabs.student;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.ArrayList;

public class StudentService {
	public ArrayList<Student> listStudent=new ArrayList<Student>();

	public void addStudent(Student student) {
		listStudent.add(student);
		save();

		/*
		 * try { FileOutputStream fileOutputStream = new FileOutputStream(
		 * "Data/StudentDetails.ser", true); ObjectOutputStream
		 * objectOutputStream = new ObjectOutputStream( fileOutputStream);
		 * objectOutputStream.writeObject(listStudent);
		 * objectOutputStream.close(); } catch (Exception e) {
		 * System.out.println("" + e.toString()); }
		 */
	}

	public void save() {
		try {
			FileOutputStream fileOutputStream = new FileOutputStream(
					"Data/StudentDetails.ser", true);
			ObjectOutputStream objectOutputStream = new ObjectOutputStream(
					fileOutputStream);
			objectOutputStream.writeObject(listStudent);
			objectOutputStream.close();
		} catch (Exception e) {
			System.out.println("" + e.toString());
		}
	}

	public ArrayList<Student> get() {
		try {
			FileInputStream fileInputStream = new FileInputStream(
					"Data/StudentDetails.ser");
			ObjectInputStream objectInputStream = new ObjectInputStream(
					fileInputStream);
			Object obj=objectInputStream.readObject();
			Student stu=(Student) obj;
			//listStudent = (ArrayList<Student>) objectInputStream.readObject();
			listStudent.add(stu);
			objectInputStream.close();
		} catch (Exception e) {
			System.out.println("error:" + e.getMessage());
		}
		return listStudent;

	}

	public void Search(String name) {
		ArrayList<Student> studentNames = new ArrayList<Student>();
		for (Student s : listStudent) {
			if (name.equalsIgnoreCase(s.getFirstName())) {
				studentNames.add(s);
			} else {
				System.out.println("No data found");
			}
		}

		for (Student s : studentNames) {
			System.out.println("StudentName : " + s.getFirstName() + " "
					+ s.getLastName() + " \nAddress : " + s.getAddress() + "");
		}
	}

}

package com.techlabs.student;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.ArrayList;

import com.techlabs.studentstore.IStudentStore;

public class StudentBinaryStore implements IStudentStore {
	private ArrayList<Student> studentlist = new ArrayList<Student>();

	public StudentBinaryStore() {
		init();
	}

	@SuppressWarnings({ "unchecked", "resource" })
	public void init() {
		try {
			FileInputStream fileInputStream = new FileInputStream(
					"Data/studentdata.ser");
			studentlist = (ArrayList<Student>) new ObjectInputStream(
					fileInputStream).readObject();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	public ArrayList<Student> get() {
		return studentlist;
	}

	@Override
	public void add(Student student) {
		studentlist.add(student);
		save();
	}

	private void save() {
		try {
			FileOutputStream fileOutputStream = new FileOutputStream(
					"Data/studentdata.ser");
			ObjectOutputStream objectOutputStream = new ObjectOutputStream(
					fileOutputStream);
			objectOutputStream.writeObject(studentlist);
			objectOutputStream.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	public Student search(String name) {
		for (Student studentName : studentlist) {
			if (name.equals(studentName.getFirstName())) {
				return studentName;
			}
		}
		return null;
	}

	@Override
	public String delete(long id) {
		for (Student student : studentlist) {
			if (id == student.getId()) {
				studentlist.remove(student);
				save();
				return "Student Delete By ID :" + student.getFirstName();
			}
		}
		return "No Student found with this ID";
	}

	@Override
	public void export() {

		try {
			FileReader fileReader = new FileReader("Resume/index.html");
			BufferedReader bufferedReader = new BufferedReader(fileReader);

			String currntLine = "";
			String htmlLine = "";

			while ((currntLine = bufferedReader.readLine()) != null) {
				htmlLine = htmlLine + currntLine + "\n";
			}
			for (Student student : studentlist) {
				htmlLine = htmlLine.replace("$name", student.getFirstName()
						+ student.getLastName());
				htmlLine = htmlLine.replace("$jobTitle", student.getAddress());
				String resumeFileName = student.getFirstName()
						.replace(" ", "-") + ".html";
				FileWriter writer = new FileWriter(
						"Resume/" + resumeFileName);
				writer.write(htmlLine);
				writer.close();

			}

		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}

package com.techlabs.student;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;

import com.techlabs.studentstore.IStudentStore;

public class StudentCsvStore implements IStudentStore {

	private ArrayList<Student> studentlist = new ArrayList<Student>();

	public StudentCsvStore() {
		init();
	}

	public void init() {
		try {

			FileReader fileReader = new FileReader("Data/studentCsvData.csv");
			BufferedReader bufferedReader = new BufferedReader(fileReader);
			String line;
			while ((line = bufferedReader.readLine()) != null) {
				String[] studentarray = line.split(",");
				studentlist.add(new Student(studentarray[0], studentarray[1],
						studentarray[2], Long.parseLong(studentarray[3])));
			}

			bufferedReader.close();
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

			BufferedWriter buffWriter = new BufferedWriter(new FileWriter(
					"Data/studentCsvData.csv"));
			for (Student student : studentlist) {
				String temp = student.getFirstName() + student.getLastName()
						+ "," + student.getAddress() + "," + student.getId();
				buffWriter.append(temp);
				buffWriter.newLine();
			}
			buffWriter.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	@Override
	public Student search(String name) {
		for (Student studentName : studentlist) {
			if (name.equals(studentName.getFirstName()))
				return studentName;
		}
		return null;
	}

	@Override
	public String delete(long id) {
		for (Student studentName : studentlist) {
			if (id == studentName.getId()) {
				studentlist.remove(studentName);
				save();
				return "Student Delete by id :" + studentName.getFirstName();
			}
		}
		return "No Student found with this id";
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

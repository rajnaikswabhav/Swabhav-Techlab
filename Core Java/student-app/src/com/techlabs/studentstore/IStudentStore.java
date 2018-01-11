package com.techlabs.studentstore;

import java.util.ArrayList;

import com.techlabs.student.Student;

public interface IStudentStore {

	public void init();

	public void add(Student student);

	public ArrayList<Student> get();

	public Student search(String name);

	public String delete(long id);

	public void export();

}

package com.techlabs.studentstore;

import com.techlabs.student.Student;

public interface StudentStore {
	
	public void addStudent(Student student);
	public void search(String name);
	public void delete(int id);

}

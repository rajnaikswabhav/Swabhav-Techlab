package com.tecklabs.student.test;

import java.util.HashSet;
import java.util.Set;

import com.techlabs.student.Student;

public class TestStudent {

	public static void main(String[] args) {

		Student student = new Student(101, 5, "Rakesh");
		Set<Student> setOfStudents=new HashSet<Student>();
		setOfStudents.add(student);
		
		Student student2 = new Student(101, 5, "Rakesh");
		setOfStudents.add(student2);
		
		print(setOfStudents);
	}
	
	public static void print(Set<Student> setStudents)
	{
		for(Student s:setStudents)
		{
			System.out.println("RollNo: "+s.getRollNo()+ " Satndard: "+s.getStandard()+ " Name: "+s.getName());
		}
	}

}

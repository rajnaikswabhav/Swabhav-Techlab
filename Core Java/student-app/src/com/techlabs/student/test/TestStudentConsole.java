package com.techlabs.student.test;

import com.techlabs.student.StudentBinaryStore;
import com.techlabs.student.StudentConsole;
import com.techlabs.student.StudentCsvStore;

public class TestStudentConsole {

	public static void main(String[] args) {

		StudentConsole studentConsole = new StudentConsole(new StudentBinaryStore());
		//StudentConsole studentConsole = new StudentConsole(new StudentCsvStore());
		studentConsole.start();
	}

}

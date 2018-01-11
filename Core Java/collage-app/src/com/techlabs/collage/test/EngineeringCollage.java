package com.techlabs.collage.test;

import com.techlabs.collage.Professor;
import com.techlabs.collage.Student;
import com.techlabs.enums.Branch;

public class EngineeringCollage {

	public static void main(String[] args) {

		Professor prof = new Professor(101, "Borivali,Mumbai", "20/04/1967");
		prof.calculateSalary(23);
		Student student = new Student(1001, "Varli,Mumbai", "14/10/1997",
				Branch.INFORMATIONTECHNOLOGY);
		Professor prof2 = new Professor(102, "Malad,Mumbai", "31/05/1972");
		prof2.calculateSalary(28);
		Student student2 = new Student(1002, "Virar,Mumbai", "19/12/1996", Branch.AUTOMOBIL);
		
		System.out.println(prof.toString());
		System.out.println(student.toString());
		System.out.println(prof2.toString());
		System.out.println(student2.toString());
	}

}

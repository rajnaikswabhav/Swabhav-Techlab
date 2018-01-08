package com.techlabs.employee.test;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;

import com.techlabs.developer.Developer;
import com.techlabs.manager.Manager;

public class TestEmployee {

	public static void main(String[] args) throws ParseException {

		DateFormat dateFormat = new SimpleDateFormat("dd/M/YYYY");
		Manager manager = new Manager("Akash", dateFormat.parse("15/5/1996"),
				5000);
		System.out.println("Age is: " + manager.calculateAge());
		System.out.println("NetSalary is: " + manager.calculateNetSalary());

		Developer developer = new Developer("Parth",
				dateFormat.parse("6/8/1995"), 10000);
		System.out.println("Age is: " + developer.calculateAge());
		System.out.println("NetSalary is: " + developer.calculateNetSalary());

	}

}

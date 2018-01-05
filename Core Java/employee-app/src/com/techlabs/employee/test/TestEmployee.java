package com.techlabs.employee.test;



import java.util.Date;

import com.techlabs.developer.Developer;
import com.techlabs.manager.Manager;

public class TestEmployee {

	public static void main(String[] args) {

		Manager manager=new Manager("Akash",new Date(15/05/1996), 5000);
		System.out.println("Age is: " +manager.calculateAge());
		System.out.println("NetSalary is: " +manager.calculateNetSalary());
		
		Developer developer=new Developer("Parth", new Date(8/06/1996),10000);
		System.out.println("Age is: " +developer.calculateAge());
		System.out.println("NetSalary is: " +developer.calculateNetSalary());
		
	}

}

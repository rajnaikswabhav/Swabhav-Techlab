package com.techlabs.employee.test;

import java.util.Set;
import java.util.StringTokenizer;
import java.util.TreeSet;

import com.techlabs.employee.EmployeeService;

public class TestEmployeeService {

	public static void main(String[] args) {
		
		EmployeeService employeeService = new EmployeeService();
		employeeService.inIt();
		employeeService.get();
		System.out.println();
		employeeService.sortByName();
		System.out.println();
		employeeService.sortBySalary();
	}
}

package com.techlabs.employee.test;


import java.util.Set;
import java.util.TreeSet;

import com.techlabs.employee.Employee;
import com.techlabs.employee.EmployeeService;
import com.techlabs.employee.SortedByNameComparator;

public class TestEmployeeService {

	public static void main(String[] args) {
		
		TestEmployeeService tEmployeeService=new TestEmployeeService();
		EmployeeService employeeService = new EmployeeService();
		employeeService.inIt();
		//tEmployeeService.sortByName(employeeService.get());
		tEmployeeService.sortBySalary(employeeService.get());
	}

	
	public void sortByName(Set<Employee> emSet) {
		System.out.println("Sorted by Name");
		TreeSet<Employee> sortedByName = new TreeSet<Employee>(
				new SortedByNameComparator());
		sortedByName.addAll(emSet);
		System.out.println(sortedByName.size());
		for(Employee emp:emSet)
		{
			System.out.println(emp.toString());
		}

	}
	
	public void sortBySalary(Set<Employee> emSet)
	{
		System.out.println();
		System.out.println("Sorted by Salary....");
		TreeSet<Employee> sortedBySalary=new TreeSet<Employee>(new SortedBySalaryCoparator());
		sortedBySalary.addAll(emSet);
		System.out.println(sortedBySalary.size());
		for(Employee emp:emSet)
		{
			System.out.println(emp.toString());
		}
	}
}

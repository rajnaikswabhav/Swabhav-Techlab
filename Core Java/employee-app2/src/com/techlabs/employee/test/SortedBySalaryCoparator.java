package com.techlabs.employee.test;

import java.util.Comparator;

import com.techlabs.employee.Employee;

public class SortedBySalaryCoparator implements Comparator<Employee>{

	@Override
	public int compare(Employee emp0, Employee emp1) {
		return emp1.getSalary().compareToIgnoreCase(emp0.getSalary());
	}

}

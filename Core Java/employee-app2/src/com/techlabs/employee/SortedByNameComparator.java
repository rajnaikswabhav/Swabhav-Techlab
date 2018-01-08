package com.techlabs.employee;

import java.util.Comparator;

public class SortedByNameComparator implements Comparator<Employee> {

	@Override
	public int compare(Employee emp1, Employee emp2) {
		return emp2.getName().compareToIgnoreCase(emp1.getName()) ;
	}

	
}

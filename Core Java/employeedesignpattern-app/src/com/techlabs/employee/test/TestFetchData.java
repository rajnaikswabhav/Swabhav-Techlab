package com.techlabs.employee.test;

import com.techlabs.employee.Employee;
import com.techlabs.employee.EmployeeDTO;
import com.techlabs.employee.HierarchyBuilder;

public class TestFetchData {

	public static void main(String[] args) {

		/*EmployeeDTO employeeData = new EmployeeDTO();
		employeeData.inIt();
		for(Employee e:employeeData.get()){
			System.out.println(e.toString());
		}*/
		
		HierarchyBuilder builder = new HierarchyBuilder();
		builder.inIt();
		builder.show();
	}

}

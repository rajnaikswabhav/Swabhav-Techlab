package com.techlabs.employee;

import java.util.List;

public class OrganizationHierarchyApp {

	private List<Employee> employeeDTO;
	private Employee president;
	private static int level = 5;
	
	public OrganizationHierarchyApp(EmployeeDTOLoader loader,
			HierarchyBuilder builder) {

		employeeDTO = loader.get();
		president = builder.buildHierarchy(employeeDTO);
	}

	public void printDetail() {

		System.out.println(president.getName());
		for (Employee employee : president.getReporteeList()) {
				System.out.println("\t"+employee.toString());
		}
	}
}

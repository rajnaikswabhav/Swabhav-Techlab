package com.techlabs.employee;

import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;

public class HierarchyBuilder {

	private Map<String, Employee> employeeMap = new LinkedHashMap<String, Employee>();
	private Employee rootEmployee;

	public Employee buildHierarchy(List<Employee> listOfEmployee) {
		for (Employee employee : listOfEmployee) {

			if (employee.getManagerID().equalsIgnoreCase("null")) {
				
				rootEmployee = employee;
				employeeMap.put(employee.getId(), rootEmployee);
			}
		}

		for (Employee employee : listOfEmployee) {
			employeeMap.put(employee.getId(), employee);
		}

		for (Employee emp : employeeMap.values()) {

			for (Employee emp1 : employeeMap.values()) {

				if (emp1.getManagerID().equalsIgnoreCase(emp.getId())) {
					emp.addReportee(emp1);
				}
			}
		}
		
		return rootEmployee;
	}
}

package com.techlabs.employee;

import java.util.ArrayList;
import java.util.List;

public class HierarchyBuilder {

	private EmployeeDTO dto;
	private List<Employee> listOfEmployee = new ArrayList<Employee>();;
	private List<Employee> employeeCompositList = new ArrayList<Employee>();

	public void inIt() {
		dto = new EmployeeDTO();
		dto.inIt();
		listOfEmployee = dto.get();
		buildHierarchy();
	}

	public void buildHierarchy() {
		for (Employee employee : listOfEmployee) {
			if (employee.getManagerID().equalsIgnoreCase("Null")) {
				employeeCompositList.add(employee);
			}
		}

		for (Employee employee : listOfEmployee) {
			if (employee.getManagerID().equalsIgnoreCase(
					employeeCompositList.get(0).getId())) {
				employeeCompositList.add(employee);
			}
		}

		for (Employee employee : listOfEmployee) {
			if (employee.getManagerID().equalsIgnoreCase(
					employeeCompositList.get(1).getId())) {
				employeeCompositList.add(employee);
			}
		}

		for (Employee employee : listOfEmployee) {
			if (employee.getManagerID().equalsIgnoreCase(
					employeeCompositList.get(2).getId())) {
				employeeCompositList.add(employee);
			}
		}

		for (Employee employee : listOfEmployee) {
			if (employee.getManagerID().equalsIgnoreCase(
					employeeCompositList.get(3).getId())) {
				employeeCompositList.add(employee);
			}
		}

	}

	public void show() {
		for (Employee e : employeeCompositList) {
			System.out.println(e.getDesignation() + " " + e.getId() + " "
					+ e.getManagerID() + " " + e.getName());
		}
		System.out.println(employeeCompositList.size());
		System.out.println(listOfEmployee.size());
	}
}

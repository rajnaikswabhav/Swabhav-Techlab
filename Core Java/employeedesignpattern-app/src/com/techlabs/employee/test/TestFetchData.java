package com.techlabs.employee.test;

import com.techlabs.employee.EmployeeDTOLoader;
import com.techlabs.employee.HierarchyBuilder;
import com.techlabs.employee.OrganizationHierarchyApp;

public class TestFetchData {

	public static void main(String[] args) {

		OrganizationHierarchyApp app = new OrganizationHierarchyApp(
				new EmployeeDTOLoader(), new HierarchyBuilder());
		app.printDetail();
		
		
		
	}

}

package com.techlabs.employee;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.io.IOException;
import java.util.List;

public class OrganizationHierarchyApp {

	private List<Employee> employeeDTO;
	private Employee president;
	private String xmlDetails;

	public OrganizationHierarchyApp(EmployeeDTOLoader loader,
			HierarchyBuilder builder) {

		employeeDTO = loader.get();
		president = builder.buildHierarchy(employeeDTO);
	}

	public void printDetail() {

		System.out.println(president.getName());
		for (Employee employee : president.getReporteeList()) {
			System.out.println("\t" + employee.toString());
			if (employee.getReporteeList() != null) {
				for (Employee employee2 : employee.getReporteeList()) {
					System.out.println("\t\t" + employee2.toString());
					if (employee2.getReporteeList() != null) {
						for (Employee employee3 : employee2.getReporteeList()) {
							System.out.println("\t\t\t" + employee3.toString());
							if (employee3.getReporteeList() != null) {
								for (Employee employee4 : employee3
										.getReporteeList()) {
									System.out.println("\t\t\t\t"
											+ employee4.toString());
								}
							}
						}
					}
				}
			}
		}
	}

	public String parseToXML() {
		xmlDetails = "<employees>\n";
		xmlDetails += "\t<" + president.getName() + ">\n";

		for (Employee employee : president.getReporteeList()) {
			xmlDetails += "\t\t<" + employee.getName() + ">\n";
			if (employee.getReporteeList() != null) {
				for (Employee employee2 : employee.getReporteeList()) {
					xmlDetails += "\t\t\t<" + employee2.getName() + ">\n";
					if (employee2.getReporteeList() != null) {
						for (Employee employee3 : employee2.getReporteeList()) {
							xmlDetails += "\t\t\t\t<" + employee3.getName()
									+ ">\n";
							if (employee3.getReporteeList() != null) {
								for (Employee employee4 : employee3
										.getReporteeList()) {
									xmlDetails += "\t\t\t\t\t<"
											+ employee4.getName() + ">";
									xmlDetails += "\t\t\t\t\t</"
											+ employee4.getName() + ">\n";
								}
							}
							xmlDetails += "\t\t\t\t</" + employee3.getName()
									+ ">\n";
						}
					}
					xmlDetails += "\t\t\t</" + employee2.getName() + ">\n";
				}
			}
			xmlDetails += "\t\t</" + employee.getName() + ">\n";
		}
		xmlDetails += "\t</" + president.getName() + ">\n";
		xmlDetails += "</employees>";

		return xmlDetails;
	}

	public void save() {

		try {
			FileWriter writer = new FileWriter("Data/hierarchyDetails.xml");
			BufferedWriter bufferedWriter = new BufferedWriter(writer);
			bufferedWriter.write(xmlDetails);
			bufferedWriter.close();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}

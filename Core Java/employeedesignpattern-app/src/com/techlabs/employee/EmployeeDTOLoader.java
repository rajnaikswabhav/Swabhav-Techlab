package com.techlabs.employee;

import java.io.BufferedReader;
import java.io.DataInputStream;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class EmployeeDTOLoader {

	private Employee employee;
	private List<Employee> employeeList = new ArrayList<Employee>();
	private Set<String> employeeSet = new HashSet<String>();

	public void inIt() {
		try {
			FileInputStream fileInputStream = new FileInputStream(
					"Data/dataFile.txt");
			DataInputStream dataInputStream = new DataInputStream(
					fileInputStream);
			BufferedReader bufferedReader = new BufferedReader(
					new InputStreamReader(dataInputStream));
			String detailOfEmployees;
			while ((detailOfEmployees = bufferedReader.readLine()) != null) {

				employeeSet.add(detailOfEmployees);

			}
			dataInputStream.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	public List<Employee> get() {
		inIt();
		for (String detail : employeeSet) {
			String[] details = detail.split(",");
			details[1] = details[1].replaceAll("'", "");
			employee = new Employee(details[0], details[1], details[3]);
			employeeList.add(employee);
		}

		return employeeList;
	}

}

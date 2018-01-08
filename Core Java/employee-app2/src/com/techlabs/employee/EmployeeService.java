package com.techlabs.employee;

import java.io.BufferedReader;
import java.io.DataInputStream;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.HashSet;
import java.util.Set;
import java.util.StringTokenizer;
import java.util.TreeSet;

public class EmployeeService {
	Employee employee;
	Set<String> employeeDetails = new HashSet<String>();
	Set<Employee> employees = new HashSet<Employee>();

	public void inIt() {
		try {
			FileInputStream fileInputStream = new FileInputStream(
					"Data/dataFile.txt");
			DataInputStream dataInputStream = new DataInputStream(
					fileInputStream);
			BufferedReader bufferedReader = new BufferedReader(
					new InputStreamReader(dataInputStream));
			String details;
			while ((details = bufferedReader.readLine()) != null) {
				employeeDetails.add(details);

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

	public Set<Employee> get() {
		/*
		 * for (String str : employeeDetails) { StringTokenizer stringTokenizer
		 * = new StringTokenizer(str); while (stringTokenizer.hasMoreTokens()) {
		 * employeeDetails.ad(stringTokenizer.nextToken("" + "")); } }
		 */
		for (String str : employeeDetails) {
			String[] name = str.split(",");
			name[1] = name[1].replaceAll("'", "");
			name[2]=name[2].replaceAll("'", "");
			name[4]=name[4].replaceAll("'", "");
			name[6]=name[6].replaceAll(name[6], name[7]);
			employee = new Employee(name[0], name[1], name[2], name[3],
					name[4], name[5], name[6]);
			employees.add(employee);
		}
		
		return employees;
	}
}

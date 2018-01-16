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

public class EmployeeDTO {

	private Employee employee;
	private List<Employee> employeeList = new ArrayList<Employee>();;
	private Set<String> employeeSet = new HashSet<String>();

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

				employeeSet.add(details);

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
		for (String str : employeeSet) {
			String[] name = str.split(",");
			name[1] = name[1].replaceAll("'", "");
			name[2] = name[2].replaceAll("'", "");
			name[4] = name[4].replaceAll("'", "");
			name[6] = name[6].replaceAll(name[6], name[7]);
			employee = new Employee(name[0], name[1], name[2], name[3],
					name[4], name[5], name[6]);
			employeeList.add(employee);
		}

		return employeeList;
	}

}

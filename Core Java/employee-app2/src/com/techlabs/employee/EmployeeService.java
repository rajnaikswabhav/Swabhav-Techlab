package com.techlabs.employee;

import java.io.BufferedReader;
import java.io.DataInputStream;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Set;
import java.util.StringTokenizer;
import java.util.TreeSet;

public class EmployeeService {
	Employee employee;
	Set<String> employeeDetails = new TreeSet<String>();
	Set<String> employeeName = new TreeSet<String>();
	Set<String> employeeSalary = new TreeSet<String>();
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

	public void get() {
		for (String str : employeeDetails) {
			StringTokenizer stringTokenizer = new StringTokenizer(str);
			while (stringTokenizer.hasMoreTokens()) {
				System.out.println(stringTokenizer.nextToken("" + ""));
			}
		}
	}

	public void sortByName() {
		for (String str : employeeDetails) {
			String[] name = str.split(",");
			name[1] = name[1].replaceAll("'", "");
			employee = new Employee(name[0], name[1], name[2], name[3],
					name[4], name[5], name[6]);
			employeeName.add(name[1]);
		}
		for (String str : employeeName) {
			System.out.println(str);
		}
	}
	
	public void sortBySalary(){
		for (String str : employeeDetails) {
			String[] name = str.split(",");
			name[1] = name[1].replaceAll("'", "");
			employee = new Employee(name[0], name[1], name[2], name[3],
					name[4], name[5], name[6]);
			employeeSalary.add(name[3]);
		}
		for (String str : employeeSalary) {
			System.out.println(str);
		}
	}

}

package com.techlabs.property;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;

public class LoadPropertyFile {

	public static void main(String[] args) {

		Properties properties = new Properties();
		InputStream inputStream;

		try {
			inputStream = new FileInputStream("PropertyFile/config-properties");

			properties.load(inputStream);

			System.out.println(properties.getProperty("database"));
			System.out.println(properties.getProperty("databaseUser"));
			System.out.println(properties.getProperty("databasePassword"));
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}

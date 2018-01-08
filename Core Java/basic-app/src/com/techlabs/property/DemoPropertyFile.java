package com.techlabs.property;

import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.util.Properties;

public class DemoPropertyFile {

	public static void main(String[] args) {
		
		Properties properties=new Properties();
		OutputStream outputStream;
		
		try {
			outputStream=new FileOutputStream("PropertyFile/config-properties");
			
			properties.setProperty("database", "MySql");
			properties.setProperty("databaseUser", "myDatabase");
			properties.setProperty("databasePassword", "password");
			
			properties.store(outputStream, null);
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}

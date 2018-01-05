package com.techlabs.annotation;

import java.lang.reflect.*;

public class CountMethodAndConstructor {

	public static void main(String[] args) {
		try {
			Class classForName = Class.forName("java.lang.Object");
			String className = classForName.getName();
			Class countClass = className.getClass();
			Constructor[] constructors = classForName
					.getDeclaredConstructors();
			Method[] methods = countClass.getDeclaredMethods();
			Field[] fields = countClass.getFields();
			System.out.println("Name of the Class\t: " + className);
			System.out.println("Number of Constructors\t: "
					+ constructors.length);
			System.out.println("Number of Methods\t: " + methods.length);
			System.out.println("Number of Fields\t: " + fields.length);

		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}

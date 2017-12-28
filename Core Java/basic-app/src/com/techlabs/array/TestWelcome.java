package com.techlabs.array;

public class TestWelcome {

	public static void main(String[] names) {

		System.out.println("This is Test class....");
		System.out.println(names.length);
		if (names.length > 0) {
			for (String name : names) {
				System.out.println("Hello Mr. " + name);
			}
		} else {
			System.out.println("Hello Mr. Akash");
		}

	}
}

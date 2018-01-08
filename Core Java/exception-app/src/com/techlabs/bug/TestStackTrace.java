package com.techlabs.bug;

public class TestStackTrace {

	public static void main(String[] args) {

		try {
			methodOne();
		} catch (Exception e) {
			e.printStackTrace();
		}
		System.out.println("End of Main...");
	}

	public static void methodOne() throws Exception {
		methodTwo();
	}

	public static void methodTwo() throws Exception {
		methodThree();
	}

	public static void methodThree() throws Exception {
		throw new Exception("Something went wrong...");
	}
}

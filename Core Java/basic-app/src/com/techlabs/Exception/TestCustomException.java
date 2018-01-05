package com.techlabs.Exception;

public class TestCustomException {

	public static void main(String[] args)  {
		int a = 10;
		int b = 0;
		validate(a, b);
	}

	public static void validate(int a, int b) {
		int c = a / b;
		System.out.println("" + c);

	}
}

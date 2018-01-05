package com.techlabs.annotation;

public class CustomAnnotation {

	public static void main(String[] args) {
		int a = 19;
		int b = 25;
		CustomAnnotation customAnnotation = new CustomAnnotation();
		customAnnotation.sum(a, b);
	}

	@NeedRefectoring
	public void sum(int a, int b) {
		int c = a + b;
		System.out.println("Sum is: " + c);
	}
}

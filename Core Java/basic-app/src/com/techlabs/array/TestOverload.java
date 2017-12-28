package com.techlabs.array;

public class TestOverload {

	public static void main(String[] args) {

		print();
		print('*');
		print(10);
		print(23.4f);
		print(20.98);
	}

	private static void print() {
		System.out.println("-------------------");
	}

	private static void print(char pattern) {
		for (int i = 0; i < 20; i++) {
			System.out.print(pattern);
		}
		System.out.println();
	}

	private static void print(int number) {
		System.out.println("Integer number is:" + number);
	}

	private static void print(float number) {
		System.out.println("Float number is:" + number);
	}

	private static void print(Double number) {
		System.out.println("Double number is:" + number);
	}
}

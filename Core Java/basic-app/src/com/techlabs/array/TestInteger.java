package com.techlabs.array;

public class TestInteger {

	public static void main(String[] args) {
		int mark = 100;
		int[] marks = { 10, 20, 30, 40 };
		changeMark(mark);
		System.out.println(mark);
		changeMarks(marks);
		for (int m : marks) {
			System.out.print(m + ",");
		}

	}

	private static void changeMark(int mark) {
		mark = 0;
	}

	private static void changeMarks(int[] marks) {
		for (int i = 0; i < marks.length; i++) {
			marks[i] = 0;
		}
	}

}

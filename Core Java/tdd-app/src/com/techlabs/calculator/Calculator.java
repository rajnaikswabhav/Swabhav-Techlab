package com.techlabs.calculator;

public class Calculator {

	public int squareOdd(int num) {
		if (num % 2 == 0) {
			throw new RuntimeException();
		} else if (num <= 0) {
			throw new RuntimeException();
		}
		return num * num;

	}

}

package com.techlabs.calc.test;

import static org.junit.Assert.*;

import org.junit.Test;

import com.techlabs.calculator.Calculator;

public class TestCalculator {

	@Test
	public void shouldSquare_oddnumbers() {

		// Arrange
		Calculator calc = new Calculator();
		int expected_Result = 9;

		// Act
		int actual = calc.squareOdd(3);

		// Assert
		assertEquals(expected_Result, actual);
	}

	@Test
	public void shouldThrownException_OnSquareEvenNumber() {
		Calculator calc = new Calculator();
		boolean hasException = false;
		try {
			int result = calc.squareOdd(6);
		} catch (Exception e) {
			hasException = true;
		}

		assertTrue(hasException);
	}

	@Test
	public void shouldThorownException_OnSqureNegativeNumbers() {
		Calculator calc = new Calculator();
		boolean hasException = false;
		try {
			int result = calc.squareOdd(-3);
		} catch (Exception e) {
			hasException = true;
		}

		assertTrue(hasException);
	}

}

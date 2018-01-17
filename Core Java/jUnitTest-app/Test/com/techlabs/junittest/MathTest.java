package com.techlabs.junittest;

import static org.junit.Assert.*;

import org.junit.Test;

import com.techlabs.calculator.Calculator;

public class MathTest {

	public void add(){
		Calculator calculator = new Calculator();
		int expectedResult = 9;
		int actualResult = calculator.add(5, 4);
		
		assertEquals(actualResult, expectedResult);
	}

}

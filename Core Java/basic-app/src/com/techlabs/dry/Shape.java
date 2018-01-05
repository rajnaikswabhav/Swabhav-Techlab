package com.techlabs.dry;

public abstract class Shape {

	protected double length;
	protected double width;

	Shape(double num, double num1) {
		length = num;
		width = num1;
	}

	abstract double area();

}

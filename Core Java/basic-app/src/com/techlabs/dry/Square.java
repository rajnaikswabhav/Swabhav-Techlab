package com.techlabs.dry;


public class Square extends Shape {
	Square(double num1) {
		super(num1, num1);
	}

	double area() {
		System.out.print("Area of squre is:");
		return (length * width);
	}
}

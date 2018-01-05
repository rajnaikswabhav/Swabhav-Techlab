package com.techlabs.dry;


public class Triangle extends Shape {
	Triangle(double num, double num1) {
		super(num, num1);
	}

	double area() {
		System.out.print("Area of Triangle is:");
		return (0.5 * length * width);
	}
}

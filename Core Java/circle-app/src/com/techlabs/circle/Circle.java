package com.techlabs.circle;

public class Circle {
	private final float radius;
	BorderStyleOption borderStyle;

	public Circle(float radius) {
		this.radius = radius;
		System.out.println("Radius is: " + radius);
		borderStyle = borderStyle.SINGLE;
	}

	public Circle(float radius, BorderStyleOption borderStyleOption) {
		this.radius = radius;
		borderStyle = borderStyleOption;
	}

	public float calculateArea() {
		float area = 22.7f * (radius * radius);
		return area;
	}

	public float getRadius() {
		return radius;
	}

	public BorderStyleOption getBorderStyleOption() {
		return borderStyle;
	}
}

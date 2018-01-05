package com.techlabs.circle.test;

import com.techlabs.circle.BorderStyleOption;
import com.techlabs.circle.Circle;

public class CircleTest {

	public static void main(String[] args) {
		Circle circle1=new Circle(3.5f);
		printDetails(circle1);
		Circle circle2=new Circle(3.5f,BorderStyleOption.DOUBLE);
		printDetails(circle2);
	}
	
	public static void printDetails(Circle circle)
	{
		System.out.println("Radius: " +circle.getRadius());
		System.out.println("BorderStyle: " +circle.getBorderStyleOption());
		System.out.println("Area: " +circle.calculateArea());
		System.out.println("HashCode: " +circle.hashCode());
	}
}

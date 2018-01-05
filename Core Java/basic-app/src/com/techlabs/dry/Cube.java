package com.techlabs.dry;


public class Cube extends Shape {
	Cube(double num)
	{
		super(num,num);
	}
	double area()
	{
		System.out.print("Area of cube is:");
		return(length*length*length);
	}
}

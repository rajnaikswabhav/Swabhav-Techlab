package com.techlabs.dry;


public class CalculateArea {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		double a, b;
		a = 13.5;
		b = 10;
		// shape1 e;
		Square square = new Square(a);
		System.out.println(square.area());
		Triangle triangle = new Triangle(a, b);
		System.out.println(triangle.area());
		Cube cube= new Cube(a);
		System.out.println(cube.area());
	}

}

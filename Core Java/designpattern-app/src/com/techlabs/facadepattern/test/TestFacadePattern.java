package com.techlabs.facadepattern.test;

import com.techlabs.facadepattern.ShapeMaker;

public class TestFacadePattern {

	public static void main(String[] args) {

		ShapeMaker shapeMaker = new ShapeMaker();
		
		shapeMaker.drawSquare();
		shapeMaker.drawCircle();
		shapeMaker.drawRectangle();
	}

}

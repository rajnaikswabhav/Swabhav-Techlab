package com.techlabs.Abstract;

abstract class Demo {
	public abstract void abMethod();
}

public class TestAbstract extends Demo {

	public static void main(String[] args) {
		TestAbstract testAbstract = new TestAbstract();
		testAbstract.abMethod();

	}

	@Override
	public void abMethod() {
		System.out.println("Abstract class Sucessfully Created....");

	}

}

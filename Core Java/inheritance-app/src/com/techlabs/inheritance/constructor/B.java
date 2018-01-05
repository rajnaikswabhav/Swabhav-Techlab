package com.techlabs.inheritance.constructor;

public class B extends A {

	public B() {
		super(10);
		System.out.println("B is created: ");
	}

	public B(int foo) {
		super(foo);
		System.out.println("B is created: ");
	}
}

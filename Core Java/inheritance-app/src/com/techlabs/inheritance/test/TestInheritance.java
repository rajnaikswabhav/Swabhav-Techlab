package com.techlabs.inheritance.test;

import com.techlabs.infant.Infant;
import com.techlabs.kid.Kid;
import com.techlabs.men.Men;
import com.tecklabs.boy.Boy;

public class TestInheritance {

	public static void main(String[] args) {
		// case1();
		//case2();
		// case3();
		 case4();
	}

	public static void case1() {
		Men x;
		x = new Men();
		x.read();
		x.play();
	}

	public static void case2() {
		Boy y;
		y = new Boy();
		y.eat();
		y.play();
	}

	public static void case3() {
		Men x;
		x = new Boy();
		x.play();
		x.read();
	}

	public static void case4() {
		atThePark(new Men());
		atThePark(new Boy());
		atThePark(new Kid());
		atThePark(new Infant());
	}

	public static void atThePark(Men x) {
		System.out.println("At the park: ");
		x.play();
	}

}

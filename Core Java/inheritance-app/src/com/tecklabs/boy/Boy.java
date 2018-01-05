package com.tecklabs.boy;

import com.techlabs.men.Men;

public class Boy extends Men {

	public Boy(){
		System.out.println("Boy is created....");
	}
	public void eat() {
		System.out.println("This is Eat Method of Boy class....");
	}

	@Override
	public void play() {
		System.out.println("This is play method of Boy class....");
	}
}

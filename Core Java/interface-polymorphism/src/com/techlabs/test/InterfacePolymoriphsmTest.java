package com.techlabs.test;

import com.techlabs.boy.Boy;
import com.techlabs.iemotionable.IEMotionable;
import com.techlabs.imannerable.IMAnnerable;
import com.techlabs.men.Men;

public class InterfacePolymoriphsmTest {

	public static void main(String[] args) {
		Men men = new Men();
		Boy boy = new Boy();

		atTheParty(men);
		atTheParty(boy);
		atTheMovie(boy);
		//atTheMovie(men);
	}

	public static void atTheMovie(IEMotionable obj) {
		System.out.println("In the Movie...");
		obj.cry();
		obj.laugh();
	}

	public static void atTheParty(IMAnnerable obj) {
		System.out.println("At the Party....");
		obj.depart();
		obj.wish();
	}

}

package com.techlabs.men;

import com.techlabs.imannerable.IMAnnerable;

public class Men implements IMAnnerable {

	@Override
	public void wish() {
		System.out.println("Men is wishing....");
	}

	@Override
	public void depart() {
		System.out.println("Men is upto Depart.....");
		
	}

}

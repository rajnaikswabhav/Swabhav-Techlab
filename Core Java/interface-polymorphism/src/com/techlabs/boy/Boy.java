package com.techlabs.boy;

import com.techlabs.iemotionable.IEMotionable;
import com.techlabs.imannerable.IMAnnerable;

public class Boy implements IEMotionable,IMAnnerable {

	@Override
	public void wish() {
		System.out.println("Boy is wishing.....");
	}

	@Override
	public void depart() {
		System.out.println("Boy is upto depart....");
	}

	@Override
	public void cry() {
		System.out.println("Boy is crying....");
	}

	@Override
	public void laugh() {
		System.out.println("Boy is Laughing....");
	}

}

package com.techlabs.simplefactory.test;

import com.techlabs.simplefactory.TVMODE;
import com.techlabs.simplefactory.TVSimplefactory;

public class TestSimpleFactory {

	public static void main(String[] args) {

		TVSimplefactory simplefactory = new TVSimplefactory();
		simplefactory.getTv(TVMODE.LED).create();
	}

}

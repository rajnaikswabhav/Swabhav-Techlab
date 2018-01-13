package com.techlabs.simplefactory;

public class TVSimplefactory {

	public ITV getTv(TVMODE mode) {
		if (mode.equals(TVMODE.LED)) {
			return new ITV() {

				@Override
				public void create() {
					System.out.println("LED TV...");
				}
			};
		}

		return null;
	}
}

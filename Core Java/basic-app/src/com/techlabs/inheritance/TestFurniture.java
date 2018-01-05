package com.techlabs.inheritance;

public class TestFurniture {

	public static void main(String[] args) {
		Chair chair = new Chair();
		chair.setType("Sag");
		chair.setWidth(20);
		chair.setHeight(30);
		chair.setLags(4);
		chair.showDetail();
		
		BookSelf bookSelf = new BookSelf();
		bookSelf.setType("Sag");
		bookSelf.setWidth(30);
		bookSelf.setHeight(50);
		bookSelf.setNumber(3);
		bookSelf.showDetail();
	}

}

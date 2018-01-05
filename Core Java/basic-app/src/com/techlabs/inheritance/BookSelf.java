package com.techlabs.inheritance;

public class BookSelf extends Furniture{
	private int number;
	
	public int getNumber() {
		return number;
	}
	
	public void setNumber(int number) {
		this.number = number;
	}
	
	public void showDetail()
	{
		System.out.println("-----------BookSelf Details----------------");
		System.out.println("Type is:"+getType());
		System.out.println("Weight is:"+getWidth());
		System.out.println("Height is:"+getHeight());
		System.out.println("No.of legs is:"+getNumber());
	}
}

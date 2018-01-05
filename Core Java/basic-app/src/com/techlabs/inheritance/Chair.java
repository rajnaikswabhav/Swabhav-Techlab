package com.techlabs.inheritance;

public class Chair extends Furniture {
	private int lags;
	
	public int getLags() {
		return lags;
	}
	
	public void setLags(int lags) {
		this.lags = lags;
	}
	
	public void showDetail()
	{
		System.out.println("-----------Chair Details----------------");
		System.out.println("Type is:"+getType());
		System.out.println("Weight is:"+getWidth());
		System.out.println("Height is:"+getHeight());
		System.out.println("No.of legs is:"+getLags());
	}
}

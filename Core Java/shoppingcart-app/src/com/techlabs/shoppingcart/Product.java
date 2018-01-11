package com.techlabs.shoppingcart;

public class Product {

	private int id;
	private String productName;
	private double cost;
	private float discount;
	private double discountCost=1;

	public Product(int id,String productName,double cost,float discount) {

		this.id=id;
		this.productName=productName;
		this.cost=cost;
		this.discount =discount;
	}
	
	public double calculateDiscountCost()
	{
		discountCost=cost - (cost*discount)/100;
		return discountCost;
	}

	public double getCost() {
		return cost;
	}

	public float getDiscount() {
		return discount;
	}

	public int getId() {
		return id;
	}

	public String getProductName() {
		return productName;
	}
	
	public double getDiscountCost() {
		return discountCost;
	}
}

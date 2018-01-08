package com.techlabs.collection;

public class LineItem {

	private int id;
	private String product;
	private int quantity;
	private int unitPrice;
	
	public LineItem(int id,String product,int quantity,int unitPrice)
	{
		this.id=id;
		this.product=product;
		this.quantity=quantity;
		this.unitPrice=unitPrice;
	}

	public int getId() {
		return id;
	}

	public String getProduct() {
		return product;
	}

	public int getQuantity() {
		return quantity;
	}

	public int getUnitPrice() {
		return unitPrice;
	}
	
	public double calculatePrice()
	{
		double price;
		price=quantity * unitPrice;
		return price;
	}
}

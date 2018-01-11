package com.techlabs.shoppingcart;

public class LineItem {

	private int id;
	private float quantity;
	private Product product;
	private double finalCost;

	public LineItem(int id, float quantity,Product product) {

		this.id = id;
		this.quantity = quantity;
		this.product=product;
	}

	public double costOfLineItem() {
		
		finalCost = product.calculateDiscountCost() * quantity;
		return finalCost;
	}

	public Product getProduct() {
		return product;
	}

	public float getQuantity() {
		return quantity;
	}

	public int getId() {
		return id;
	}
	
	public double getFinalCost() {
		return finalCost;
	}
}

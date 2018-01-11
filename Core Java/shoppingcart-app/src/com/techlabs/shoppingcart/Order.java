package com.techlabs.shoppingcart;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class Order {

	private int id;
	private Date orderDate;
	private List<LineItem> items = new ArrayList<LineItem>();;
	private double checkoutCost=0;

	public Order(int id,Date orderDate) {

		this.id=id;
		this.orderDate=orderDate;
	}
	
	public void addItem(LineItem item)
	{
		items.add(item);
	}
	
	public double calculateCheckout()
	{
		for(LineItem item:items)
		{
			checkoutCost = checkoutCost + item.costOfLineItem();
		}
		return checkoutCost;
	}

	public List<LineItem> getItems() {
		return items;
	}

	public int getId() {
		return id;
	}

	public Date getOrderDate() {
		return orderDate;
	}
	
	public double getCheckoutCost() {
		return checkoutCost;
	}
}

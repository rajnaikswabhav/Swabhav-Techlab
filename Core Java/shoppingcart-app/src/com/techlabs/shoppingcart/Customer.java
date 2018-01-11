package com.techlabs.shoppingcart;

import java.util.ArrayList;
import java.util.List;

public class Customer {

	private int id;
	private String name;
	private List<Order> orders;
	
	public Customer(int id,String name) {
		this.id=id;
		this.name=name;
	}
	public void addOrders(Order order)
	{
		orders = new ArrayList<Order>();
		orders.add(order);
	}

	public List<Order> getOrders() {
		return orders;
	}

	public String getName() {
		return name;
	}

	public int getId() {
		return id;
	}
}

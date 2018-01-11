package com.techlabs.testcases;

import static org.junit.Assert.*;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;


import org.junit.Test;

import com.techlabs.shoppingcart.LineItem;
import com.techlabs.shoppingcart.Order;
import com.techlabs.shoppingcart.Product;

public class TestOrder {
	
	@Test
	public void addItem() throws ParseException
	{
		DateFormat dateFormat = new SimpleDateFormat("dd/M/YYYY");
		Order order=new Order(1010101,dateFormat.parse("09/1/2018"));
		LineItem item = new LineItem(1011, 5, new Product(101, "Earasar", 10, 10f));
		LineItem item2 = new LineItem(011010,10,new Product(102,"Sharpner",20,10f));
		order.addItem(item);
		order.addItem(item2);
		int lenghtOfItmes=order.getItems().size();
		assertTrue(lenghtOfItmes==2);
	}
	
	@Test
	public void calculateCheckout() throws ParseException
	{
		DateFormat dateFormat = new SimpleDateFormat("dd/M/YYYY");
		Order order=new Order(1010101,dateFormat.parse("09/1/2018"));
		LineItem item = new LineItem(1011, 5, new Product(101, "Earasar", 10, 10f));
		LineItem item2 = new LineItem(011010,10,new Product(102,"Sharpner",20,10f));
		double expectedCost=225.0;
		order.addItem(item);
		order.addItem(item2);
		double actualCost=order.calculateCheckout();
		
		assertTrue(actualCost==expectedCost);
	
	}
}

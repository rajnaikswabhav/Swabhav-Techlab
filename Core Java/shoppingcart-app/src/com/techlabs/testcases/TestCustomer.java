package com.techlabs.testcases;

import static org.junit.Assert.*;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;

import org.junit.Test;

import com.techlabs.shoppingcart.Customer;
import com.techlabs.shoppingcart.Order;

public class TestCustomer {

	@Test
	public void addOrder() throws ParseException
	{
		DateFormat dateFormat = new SimpleDateFormat("dd/M/YYYY");
		Customer customer=new Customer(1,"foo");
		Order order=new Order(10111,dateFormat.parse("09/1/2018"));
		
		customer.addOrders(order);
		int lengthOforders=customer.getOrders().size();
		
		assertTrue(lengthOforders==1);
	}
}

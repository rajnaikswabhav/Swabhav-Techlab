package com.techlabs.shoppingcart.test;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;

import com.techlabs.shoppingcart.LineItem;
import com.techlabs.shoppingcart.Order;
import com.techlabs.shoppingcart.Product;

public class TestOrder {

	public static void main(String[] args) throws ParseException {

		DateFormat dateFormat = new SimpleDateFormat("dd/M/YYYY");
		Order order = new Order(101011,dateFormat.parse("09/1/2018"));
		LineItem item =new LineItem(1011,5,new Product(101,"Erasar",10,10f));
		LineItem item2 = new LineItem(1012,10,new Product(102,"Sharpener",20,10f));
		order.addItem(item);
		order.addItem(item2);
		System.out.println(order.calculateCheckout());
 	}

}

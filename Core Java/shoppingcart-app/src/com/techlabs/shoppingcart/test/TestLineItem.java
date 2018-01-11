package com.techlabs.shoppingcart.test;

import com.techlabs.shoppingcart.LineItem;
import com.techlabs.shoppingcart.Product;

public class TestLineItem {

	public static void main(String[] args) {

		LineItem lineItem = new LineItem(1011,5,new Product(101,"Erasar",10,10f));
		System.out.println(lineItem.costOfLineItem());
	}

}

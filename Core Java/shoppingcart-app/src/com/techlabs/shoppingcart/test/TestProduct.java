package com.techlabs.shoppingcart.test;

import com.techlabs.shoppingcart.Product;

public class TestProduct {

	public static void main(String[] args) {

		Product product = new Product(101, "Erasar", 10, 10f);
		System.out.println(product.calculateDiscountCost());
	}

}

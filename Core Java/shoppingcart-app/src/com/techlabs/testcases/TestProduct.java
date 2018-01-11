package com.techlabs.testcases;

import static org.junit.Assert.*;

import org.junit.Test;

import com.techlabs.shoppingcart.Product;

public class TestProduct {

	@Test
	public void calculateDiscountCost()
	{
		Product product =new Product(101,"Erasar", 10, 10f);
		double discountCost=9.0;
		double actualCost = product.calculateDiscountCost();
		
		assertTrue(actualCost==discountCost);
	}


}

package com.techlabs.testcases;

import static org.junit.Assert.*;

import org.junit.Test;

import com.techlabs.shoppingcart.LineItem;
import com.techlabs.shoppingcart.Product;

public class TestLineItem {

	@Test
	public void costOfLineItem()
	{
		LineItem lineItem=new LineItem(1011,5,new Product(101, "Erasar", 10, 10f));
		double finalCost=45.0;
		double actualCost=lineItem.costOfLineItem();
		
		assertTrue(actualCost == finalCost);
		
	}

}

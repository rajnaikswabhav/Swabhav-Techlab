package com.techlabs.lineItem.test;

import java.util.Comparator;

import com.techlabs.collection.LineItem;

public class SortedByQuantityComaparator implements Comparator<LineItem> {

	@Override
	public int compare(LineItem arg0, LineItem arg1) {
		
		if(arg0.getQuantity()>arg1.getQuantity()){
			return -1;
		}
		else if(arg0.getQuantity()==arg1.getQuantity())
		{
			return 1;
		}
		else if(arg0.getQuantity()<arg1.getQuantity()){
			return 1 ;
		}
		return 1;
	}
}

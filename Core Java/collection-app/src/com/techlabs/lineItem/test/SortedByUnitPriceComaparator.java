package com.techlabs.lineItem.test;

import java.util.Comparator;

import com.techlabs.collection.LineItem;

public class SortedByUnitPriceComaparator  implements Comparator<LineItem>{

	@Override
	public int compare(LineItem arg0, LineItem arg1) {
		
		if(arg0.getUnitPrice()>arg1.getUnitPrice())
		{
			return -1;
		}
		else if(arg0.getUnitPrice()==arg1.getUnitPrice())
		{
			return -1;
		}
		else if(arg0.getUnitPrice()<arg1.getUnitPrice())
		{
			return 1;
		}
		return -1;
	}

}

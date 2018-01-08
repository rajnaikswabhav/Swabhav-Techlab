package com.techlabs.lineItem.test;

import java.util.LinkedHashSet;
import java.util.Set;

import com.techlabs.collection.LineItem;

public class TestLinkedHashSet {

	public Set<LineItem> linkedItem ;
	public static void main(String[] args) {
	
		TestLinkedHashSet teHashSet=new TestLinkedHashSet();
		teHashSet.init();
	}

	public void init()
	{
		LineItem item1 = new LineItem(101, "book", 4, 60);
		LineItem item2 = new LineItem(102, "Erasar", 2, 10);
		LineItem item3 = new LineItem(103, "Sharpener", 6, 10);
		LineItem item4 = new LineItem(104, "Compass", 5, 33);
		LineItem item5 = new LineItem(105, "Pen", 10, 10);
		
		linkedItem=new LinkedHashSet<LineItem>();
		linkedItem.add(item1);
		linkedItem.add(item2);
		linkedItem.add(item3);
		linkedItem.add(item4);
		linkedItem.add(item5);
		
		for (LineItem item : linkedItem) {
			System.out.println(item.toString());
		}
	}
	
}

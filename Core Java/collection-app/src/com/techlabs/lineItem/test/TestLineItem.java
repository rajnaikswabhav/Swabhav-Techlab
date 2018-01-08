package com.techlabs.lineItem.test;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import javax.sound.sampled.Line;

import com.techlabs.collection.LineItem;

public class TestLineItem {

	public static void main(String[] args) {
		//case1(); //not type safe
		case2();
		
	}
	
	public static void case1()
	{
		List items=new ArrayList();
		items.add(new LineItem(101,"book",5, 60).calculatePrice());
		items.add(new LineItem(102,"Eraser",4,10).calculatePrice());
		items.add(new LineItem(103,"Sharpener",6, 7).calculatePrice());
		items.add(new LineItem(104,"Compass",3, 33).calculatePrice());
		items.add(new LineItem(105,"Pen",10, 5).calculatePrice());
		items.add("Akash");
		for(Object o:items)
		{
			System.out.println(o);
		}
		/*Iterator itr=items.iterator();
		while(itr.hasNext())
		{
			System.out.println(itr.next());
		}*/
	}
	
	public static void case2()
	{
		List<LineItem> items=new ArrayList<LineItem>();
		LineItem item1=new LineItem(101,"book",5, 60);
		LineItem item2=new LineItem(102,"Erasar",4,10);
		LineItem item3=new LineItem(103,"Sharpener",6, 7);
		LineItem item4=new LineItem(104,"Compass",3, 33);
		LineItem item5=new LineItem(105,"Pen",10, 5);
		items.add(item1);
		items.add(item2);
		items.add(item3);
		items.add(item4);
		items.add(item5);
		
		for(LineItem l:items)
		{
			System.out.println("Id : "+l.getId()+ " Product : "+l.getProduct()+ " "
					+ "\tQuantity : "+l.getQuantity()+ " UnitPrice : "+l.getUnitPrice()+ " Total : "+l.calculatePrice());
		}
	}

}

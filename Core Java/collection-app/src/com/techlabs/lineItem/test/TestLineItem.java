package com.techlabs.lineItem.test;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.List;
import java.util.Set;
import java.util.TreeSet;

import com.techlabs.collection.LineItem;

public class TestLineItem {

	public static void main(String[] args) {
		// case1(); //not type safe
		// case2();
		// itemsSortedByQuantity();
		// case3();
		// sortedByUnitPrice();
		// case4();
		case5();

	}

	public static void case1() {
		List items = new ArrayList();
		items.add(new LineItem(101, "book", 5, 60).calculatePrice());
		items.add(new LineItem(102, "Eraser", 4, 10).calculatePrice());
		items.add(new LineItem(103, "Sharpener", 6, 7).calculatePrice());
		items.add(new LineItem(104, "Compass", 3, 33).calculatePrice());
		items.add(new LineItem(105, "Pen", 10, 5).calculatePrice());
		items.add("Akash");
		for (Object o : items) {
			System.out.println(o);
		}
		/*
		 * Iterator itr=items.iterator(); while(itr.hasNext()) {
		 * System.out.println(itr.next()); }
		 */
	}

	public static void case2() {
		List<LineItem> items = new ArrayList<LineItem>();
		LineItem item1 = new LineItem(101, "book", 5, 60);
		LineItem item2 = new LineItem(102, "Erasar", 4, 10);
		LineItem item3 = new LineItem(103, "Sharpener", 6, 7);
		LineItem item4 = new LineItem(104, "Compass", 3, 33);
		LineItem item5 = new LineItem(105, "Pen", 10, 5);
		items.add(item1);
		items.add(item2);
		items.add(item3);
		items.add(item4);
		items.add(item5);

		for (LineItem l : items) {
			System.out.println("Id : " + l.getId() + " Product : "
					+ l.getProduct() + " " + "\tQuantity : " + l.getQuantity()
					+ " UnitPrice : " + l.getUnitPrice() + " Total : "
					+ l.calculatePrice());
		}
	}

	public static void itemsSortedByQuantity() {
		Set<LineItem> itemSet = new HashSet<LineItem>();
		LineItem item1 = new LineItem(101, "book", 5, 60);
		LineItem item2 = new LineItem(102, "Erasar", 4, 10);
		LineItem item3 = new LineItem(103, "Sharpener", 6, 7);
		LineItem item4 = new LineItem(104, "Compass", 3, 33);
		LineItem item5 = new LineItem(105, "Pen", 10, 5);
		itemSet.add(item1);
		itemSet.add(item2);
		itemSet.add(item3);
		itemSet.add(item4);
		itemSet.add(item5);
		TreeSet<LineItem> sortedByQuantity;
		sortedByQuantity = new TreeSet<LineItem>(
				new SortedByQuantityComaparator());
		sortedByQuantity.addAll(itemSet);

		for (LineItem item : sortedByQuantity) {
			System.out.println(item.toString());
		}
	}

	public static void case3() {
		TreeSet<LineItem> itemTreeSet = new TreeSet<LineItem>(
				new SortedByQuantityComaparator());
		LineItem item1 = new LineItem(101, "book", 4, 60);
		LineItem item2 = new LineItem(102, "Erasar", 4, 10);
		LineItem item3 = new LineItem(103, "Sharpener", 6, 7);
		LineItem item4 = new LineItem(104, "Compass", 4, 33);
		LineItem item5 = new LineItem(105, "Pen", 10, 5);
		itemTreeSet.add(item1);
		itemTreeSet.add(item2);
		itemTreeSet.add(item3);
		itemTreeSet.add(item4);
		itemTreeSet.add(item5);
		for (LineItem item : itemTreeSet) {
			System.out.println(item.toString());
		}
	}

	public static void sortedByUnitPrice() {
		Set<LineItem> itemSet = new HashSet<LineItem>();
		LineItem item1 = new LineItem(101, "book", 5, 60);
		LineItem item2 = new LineItem(102, "Erasar", 4, 10);
		LineItem item3 = new LineItem(103, "Sharpener", 6, 7);
		LineItem item4 = new LineItem(104, "Compass", 3, 33);
		LineItem item5 = new LineItem(105, "Pen", 10, 5);
		itemSet.add(item1);
		itemSet.add(item2);
		itemSet.add(item3);
		itemSet.add(item4);
		itemSet.add(item5);
		TreeSet<LineItem> sortedByQuantity;
		sortedByQuantity = new TreeSet<LineItem>(
				new SortedByUnitPriceComaparator());
		sortedByQuantity.addAll(itemSet);

		for (LineItem item : sortedByQuantity) {
			System.out.println(item.toString());
		}
	}

	public static void case4() {
		TreeSet<LineItem> itemTreeSet = new TreeSet<LineItem>(
				new SortedByUnitPriceComaparator());
		LineItem item1 = new LineItem(101, "book", 4, 60);
		LineItem item2 = new LineItem(102, "Erasar", 2, 10);
		LineItem item3 = new LineItem(103, "Sharpener", 6, 10);
		LineItem item4 = new LineItem(104, "Compass", 5, 33);
		LineItem item5 = new LineItem(105, "Pen", 10, 10);
		itemTreeSet.add(item1);
		itemTreeSet.add(item2);
		itemTreeSet.add(item3);
		itemTreeSet.add(item4);
		itemTreeSet.add(item5);
		for (LineItem item : itemTreeSet) {
			System.out.println(item.toString());
		}
	}

	public static void case5() {
		Set<LineItem> linkedItemSet = new LinkedHashSet<LineItem>();
		LineItem item1 = new LineItem(101, "book", 4, 60);
		LineItem item2 = new LineItem(102, "Erasar", 2, 10);
		LineItem item3 = new LineItem(103, "Sharpener", 6, 10);
		LineItem item4 = new LineItem(104, "Compass", 5, 33);
		LineItem item5 = new LineItem(105, "Pen", 10, 10);
		linkedItemSet.add(item1);
		linkedItemSet.add(item2);
		linkedItemSet.add(item3);
		linkedItemSet.add(item4);
		linkedItemSet.add(item5);
		
		for (LineItem item : linkedItemSet) {
			System.out.println(item.toString());
		}
	}
}

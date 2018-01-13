package com.techlabs.customlinkedlist.test;

import com.techlabs.customlinkedlist.SwabhavLinkedList;

public class TestSwabhavLinkedList {

	public static void main(String[] args) {

		SwabhavLinkedList<Integer> sLinkedList = new SwabhavLinkedList<Integer>();
		sLinkedList.add(10);
		sLinkedList.add(15);
		sLinkedList.add(25);
		sLinkedList.add(30);
		sLinkedList.addLast(20, 15);
		//sLinkedList.deleteFirst();
		//sLinkedList.deleteAfter(15);
		//sLinkedList.traverse();
		sLinkedList.count();
		
		for(Integer i : sLinkedList)
		{
			System.out.println(i);
		}
	}

}

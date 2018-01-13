package com.techlabs.customlinkedlist;

import java.util.Iterator;
import java.util.ListIterator;

public class SwabhavLinkedList<T> implements Iterable<T>{

	private Node<T> head;
	private Node<T> back;
	
	public void add(T typeOfElement)
	{
		Node<T> node = new Node<T>();
		node.setValue(typeOfElement);
		System.out.println("Adding value: "+typeOfElement);
		
		if(head == null){
			head = node;
			back = node;
		}
		else{
			back.setNextNode(node);
			back = node;
		}
	}
	
	public void addLast(T type , T afterElement){
		
		Node<T> temp = head;
		Node<T> refNode = null;
		
		System.out.println("Travesing all nodes...");
		
		while(true){
			
			if(temp == null){
				break;
			}
			if(temp.compareTo(afterElement) == 0){
				refNode = temp;
				break;
			}
			temp = temp.getNextNode();
		}
		if(refNode != null){
			
			Node<T> node = new Node<T>();
			node.setValue(type);
			node.setNextNode(temp.getNextNode());
			if(temp == back){
				back = node;
			}
			temp.setNextNode(node);
		}
		else{
			System.out.println("Unable to find given element....");
		}
	}
	
	public void deleteFirst(){
		if(head == null)
		{
			System.out.println("Underflow.....");
		}
		Node<T> temp = head;
		head = temp.getNextNode();
		if(head == null){
			back = null;
		}
		
		System.out.println("Deleted: "+temp.getValue());
	}
	
	public void deleteAfter(T afterElement){
		
		Node<T> temp = head;
		Node<T> refNode = null;
		
		System.out.println("Travesing all nodes....");
		
		while(true){
			if(temp == null){
				break;
			}
			if(temp.compareTo(afterElement) == 0){
				refNode = temp;
				break;
			}
			temp=temp.getNextNode();
		}
		if(refNode != null){
			temp = refNode.getNextNode();
			refNode.setNextNode(temp.getNextNode());
			
			if(refNode == null){
				back = refNode;
			}
			System.out.println("Deleted: "+temp.getValue());
		}else{
			System.out.println("Unable to find givaen element.....");
		}
	}
	
	/*public void traverse()
	{
		Node<T> temp= head;
		while(true)
		{
			if(temp == null){
				break;
			}
			System.out.println(temp.getValue());
			temp = temp.getNextNode();
		}
	}*/
	
	public void count()
	{
		Node<T> temp = head;
		int count = 0;
		while(true){
			if(temp == null){
				break;
			}
			count++;
			temp=temp.getNextNode();
		}
		System.out.println("count is: "+count);
	}

	@Override
	public Iterator<T> iterator() {
		return new ListIterator<T>() {

			Node<T> currentNode = head;;
			private Node<T> previousNode = null;
		
			@Override
			public void add(T arg0) {
				// TODO Auto-generated method stub
				
			}

			@Override
			public boolean hasNext() {
				if(currentNode != null && currentNode.getNextNode() != null  )
				{
					return true;
				}else{
					return false;
				}
				
			}

			@Override
			public boolean hasPrevious() {
				// TODO Auto-generated method stub
				return false;
			}

			@Override
			public T next() {
				if(previousNode == null){
					previousNode = currentNode;
					return previousNode.getValue();
				}
				T node = currentNode.getValue();
				currentNode = currentNode.getNextNode();
				return currentNode.getValue();
			}

			@Override
			public int nextIndex() {
				// TODO Auto-generated method stub
				return 0;
			}

			@Override
			public T previous() {
				// TODO Auto-generated method stub
				return null;
			}

			@Override
			public int previousIndex() {
				// TODO Auto-generated method stub
				return 0;
			}

			@Override
			public void remove() {
				// TODO Auto-generated method stub
				
			}

			@Override
			public void set(T arg0) {
				// TODO Auto-generated method stub
				
			}
			
			
		};
	}
}

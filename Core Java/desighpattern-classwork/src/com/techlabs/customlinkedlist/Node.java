package com.techlabs.customlinkedlist;

public class Node<T> implements Comparable<T> {

	
	private T value;
	private Node<T> nextNode;
	
	
	public T getValue() {
		return value;
	}


	public void setValue(T value) {
		this.value = value;
	}


	public Node<T> getNextNode() {
		return nextNode;
	}


	public void setNextNode(Node<T> nextNode) {
		this.nextNode = nextNode;
	}


	@Override
	public int compareTo(T arg) {
		
		if(arg == this.value){
			return 0;
		}
		return 1;
	}

}

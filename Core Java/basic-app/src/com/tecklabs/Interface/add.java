package com.tecklabs.Interface;

public class add implements sum{

	public static void main(String[] args) {
		 int value1=30,value2=10;
		 add add=new add();
		 add.addDigit(value1, value2);
		 add.subDigit(value1, value2);
		 System.out.println("Multiplication is:" +add.mutliplication(value1, value2));
		 System.out.println("Division is:" +add.division(value1, value2));
		}

	@Override
	public void addDigit(int a,int b) {
		System.out.println("Sum is:" +(a+b));
		
	}

	@Override
	public void subDigit(int a, int b) {
		System.out.println("Sum is:" +(a-b));
		
	}

	@Override
	public int mutliplication(int a, int b) {
		return a*b;
	}

	@Override
	public double division(int a, int b) {
		return a/b;
	}

}

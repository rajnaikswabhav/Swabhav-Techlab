package com.techlabs.account;

public class Account {

	private final String name;
	private final int id;
	private double balance;
	public static int count;

	static{
		System.out.println("Inside static block....");
	}
	public Account(String name, int id, double balance) {
		System.out.println("Inside Constustor");
		this.name = name;
		this.id = id;
		this.balance = balance;
		count++;
	}

	public Account(String name, int id) {
		this(name, id, 500);
	}

	public void deposit(double balance) {
		this.balance = this.balance + balance;
	}

	public void withdraw(double balance) {
		if (this.balance >= 500 && this.balance - balance >= 500) {
			this.balance = this.balance - balance;
		} else {
			throw new RuntimeException(
					"Your balance is low Minimum balance require 500");
		}
	}

	public int getId() {
		return id;
	}

	public String getName() {
		return name;
	}

	public double getBalance() {
		return balance;
	}

	public int getCount() {
		return count;
	}

	public int getAccountNumber() {
		return getCount();
	}
	
	public static int getAccountHolder()
	{
		return count;
	}
}

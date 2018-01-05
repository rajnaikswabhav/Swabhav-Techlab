package com.techlabs.account;

public abstract class Account {

	private int accountNumber;
	private String name;
	protected double balance;

	public Account(int account, String name, double balance) {
		this.accountNumber = account;
		this.name = name;
		this.balance = balance;
	}

	public void deposit(double balance) {
		this.balance = this.balance + balance;
	}

	public abstract void withdraw(double balance);

	public int getAccount() {
		return accountNumber;
	}

	public String getName() {
		return name;
	}

	public double getBalance() {
		return balance;
	}
}

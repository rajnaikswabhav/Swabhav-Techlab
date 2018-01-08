package com.techlabs.account;

import com.techlabs.account.Account;

public class InsufficientFundException extends RuntimeException {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	private String name;
	private double balance;
	private Account account;

	public InsufficientFundException(String name, double balance,
			Account account) {
		this.name = name;
		this.balance = balance;
		this.account = account;
	}

	@Override
	public String getMessage(){
		return name + " your account has a " +account.getBalance()+ " rupee,you can't withdraw " +balance+ " Rupees..." ;  
	}
}

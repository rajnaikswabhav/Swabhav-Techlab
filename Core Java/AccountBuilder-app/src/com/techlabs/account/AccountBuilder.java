package com.techlabs.account;

public class AccountBuilder  {

	
	private int accountNumber;
	private String accountHolderName;
	private boolean loan;
	private double balance;
	
	public AccountBuilder withAccountNumber(int accountNumber){
		this.accountNumber = accountNumber;
		return this; 
	}
	
	public AccountBuilder withAccountHolderName(String accountHolderName){
		this.accountHolderName = accountHolderName;
		return this;
	}
	
	public AccountBuilder hasLoan(){
		loan = true;
		return this;
	}
	
	public AccountBuilder withBalance(double balance){
		this.balance = balance;
		return this;
	}
	
	public Account build(){
		Account account = new Account();
		account.setAccountNumber(accountNumber);
		account.setAccountHolderName(accountHolderName);
		account.setHasLoan(loan);
		account.setBalance(balance);
		return account;
	}
	

	
}

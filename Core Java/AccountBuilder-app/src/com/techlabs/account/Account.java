package com.techlabs.account;

public class Account {

	protected int accountNumber;
	protected String accountHolderName;
	protected boolean loan;
	protected double balance;
	
	public int getAccountNumber() {
		return accountNumber;
	}
	
	public void setAccountNumber(int accountNumber) {
		this.accountNumber = accountNumber;
	}
	
	public String getAccountHolderName() {
		return accountHolderName;
	}
	
	public void setAccountHolderName(String accountHolderName) {
		this.accountHolderName = accountHolderName;
	}
	
	public boolean getHasLoan() {
		return loan;
	}
	
	public void setHasLoan(boolean hasLoan) {
		this.loan = hasLoan;
	}
	
	public double getBalance() {
		return balance;
	}
	
	public void setBalance(double balance) {
		this.balance = balance;
	}
	
	@Override 
	public String toString(){
		return "Your Account Number is: "+getAccountNumber()+ "\nYour Name is: "+getAccountHolderName()+ "\n You have a loan: "
				+getHasLoan()+ "\nYour balance is: "+getBalance() ;
	}
	
}

package com.techlabs.account.test;


import com.techlabs.account.Account;
import com.techlabs.account.AccountBuilder;

public class TestAccountBuilder {

	public static void main(String[] args) {
		
		Account account = new AccountBuilder().withAccountNumber(101)
											  //.withAccountHolderName("Akash")
											 // .hasLoan()
											  .withBalance(5000)
											  .build();
		System.out.println(account.toString());
	}

}

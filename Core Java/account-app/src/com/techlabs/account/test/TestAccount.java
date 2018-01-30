package com.techlabs.account.test;

import com.techlabs.account.Account;

public class TestAccount {

	public static void main(String[] args) {

		Account account = new Account("Akash", 101, 5000);
		account.deposit(300);
		System.out.println(account.getName() + " Your Balance is: "
				+ account.getBalance());
		account.withdraw(1000);
		System.out.println(account.getName() + " Your Balance is: "
				+ account.getBalance() + "\n");

		Account account2 = new Account("Parth", 102);
		account2.deposit(2000);
		System.out.println(account2.getName() + " Your Balance is: "
				+ account2.getBalance());
		account2.withdraw(500);
		System.out.println(account2.getName() + " Your Balance is: "
				+ account2.getBalance());
		System.out.println("Total Account Holders:" + account.getCount());
		System.out.println("Total Account Holders:"+Account.getAccountHolder());
	}

}

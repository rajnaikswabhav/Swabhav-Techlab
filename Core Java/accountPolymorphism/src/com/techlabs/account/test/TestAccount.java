package com.techlabs.account.test;

import com.techlabs.account.Account;
import com.techlabs.currentAccount.CurrentAccount;
import com.techlabs.savingAccount.SavingAccount;

public class TestAccount {

	public static void main(String[] args) {
		SavingAccount savingAccount = new SavingAccount(101, "xyz", 5000);
		savingAccount.deposit(1000);
		savingAccount.withdraw(5000);
		printDetails(savingAccount);

		CurrentAccount currentAccount = new CurrentAccount(102, "pqr", 10000);
		currentAccount.deposit(2000);
		currentAccount.withdraw(7000);
		printDetails(currentAccount);

	}

	public static void printDetails(Account account) {
		System.out.println("Account Number is: " + account.getAccount());
		System.out.println("Name of Account holder is: " + account.getName());
		System.out.println("Balance is: " + account.getBalance());
	}

}

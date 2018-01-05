package com.techlabs.savingAccount;

import com.techlabs.account.Account;

public class SavingAccount extends Account {

	public SavingAccount(int account, String name, double balance) {
		super(account, name, balance);
	}

	@Override
	public void withdraw(double balance) {

		if (this.balance >= 500 && this.balance - balance >= 500) {
			this.balance = this.balance - balance;
		} else {
			throw new RuntimeException(
					"Your balance is low Minimum balance require 500");
		}
	}

}

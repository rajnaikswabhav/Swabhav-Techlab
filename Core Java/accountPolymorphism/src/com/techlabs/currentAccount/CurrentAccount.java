package com.techlabs.currentAccount;

import com.techlabs.account.Account;

public class CurrentAccount extends Account {

	public CurrentAccount(int account, String name, double balance) {
		super(account, name, balance);
	}

	@Override
	public void withdraw(double balance) {

		if (this.balance >= 5000) {
			this.balance = this.balance - balance;
		} else {
			throw new RuntimeException(
					"Your balance is low Minimum balance require 500");
		}
	}

}

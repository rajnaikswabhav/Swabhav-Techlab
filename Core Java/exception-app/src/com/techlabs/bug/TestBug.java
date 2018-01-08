package com.techlabs.bug;

import com.techlabs.account.Account;

public class TestBug {

	public static void main(String[] args) {

		Account account = new Account("Akash", 101, 5000);
		/*
		 * int a = Integer.parseInt(args[0]); int b = Integer.parseInt(args[1]);
		 * int c = a / b; System.out.println("div is: " + c);
		 */
		try {
			account.withdraw(4600);
		} catch (Exception e) {
			System.out.println(e);
		}
		finally{
			account.withdraw(1000);
		}
	}

}

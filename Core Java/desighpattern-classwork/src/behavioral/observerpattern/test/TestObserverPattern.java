package behavioral.observerpattern.test;

import behavioral.observerpattern.Account;
import behavioral.observerpattern.EmailListener;
import behavioral.observerpattern.SMSListener;

public class TestObserverPattern {

	public static void main(String[] args) {

		Account account = new Account(110, "Akash", 5000);
		account.addListener(new EmailListener());
		account.addListener(new SMSListener());
		account.deposite(2000);
		System.out.println();
	}

}

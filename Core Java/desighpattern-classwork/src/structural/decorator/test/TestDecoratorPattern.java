package structural.decorator.test;

import structural.decorator.Account;
import structural.decorator.IAccount;
import structural.decorator.LogDecorator;
import structural.decorator.EmailDecorator;
import structural.decorator.SMSDecorator;

public class TestDecoratorPattern {

	public static void main(String[] args) {

		IAccount acc = new Account(1000);
		IAccount iAccount = new EmailDecorator(new LogDecorator(acc));
		IAccount iAccount2 = new SMSDecorator(new LogDecorator(acc));
		iAccount.deposit(500);
		iAccount2.withdraw(700);
		System.out.print(acc.getBalance());
	}

}

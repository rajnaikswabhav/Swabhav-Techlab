package structural.decorator;

public class SMSDecorator extends DecoratorBase implements IAccount {

	public SMSDecorator(IAccount iAccount) {
		super(iAccount);
	}

	@Override
	public void deposit(double balance) {
		iAccount.deposit(balance);
		System.out
				.println("You are informed by SMS ,Your account balance is deposited..");
	}

	@Override
	public void withdraw(double balance) {
		iAccount.withdraw(balance);
		System.out
				.println("You are informed by SMS , Your account balance is debited...");
	}

	@Override
	public double getBalance() {
		return iAccount.getBalance();
	}

}

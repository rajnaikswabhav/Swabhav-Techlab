package structural.decorator;

public class EmailDecorator extends DecoratorBase implements IAccount {

	public EmailDecorator(IAccount iAccount) {
		super(iAccount);
	}

	@Override
	public void deposit(double balance) {
		iAccount.deposit(balance);
		System.out
				.println("We are informed you by Email,Your account balance is deposited. ");
	}

	@Override
	public void withdraw(double balance) {
		iAccount.withdraw(balance);
		System.out
				.println("We are informed you by Email,Your account balance is debited.");
	}

	@Override
	public double getBalance() {
		// TODO Auto-generated method stub
		return iAccount.getBalance();
	}

}

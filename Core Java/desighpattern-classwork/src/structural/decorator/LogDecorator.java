package structural.decorator;

public class LogDecorator extends DecoratorBase implements IAccount {

	public LogDecorator(IAccount iAccount) {
		super(iAccount);
	}

	@Override
	public void deposit(double balance) {
		System.out.println("Before Deposite : " + iAccount.getBalance());
		iAccount.deposit(balance);
		System.out.println("After deposit your balance is: "
				+ iAccount.getBalance());
	}

	@Override
	public void withdraw(double balance) {
		System.out.println("Before Withdraw : " + iAccount.getBalance());
		iAccount.withdraw(balance);
		System.out.println("After withdraw your balance is: "
				+ iAccount.getBalance());
	}

	@Override
	public double getBalance() {
		// TODO Auto-generated method stub
		return iAccount.getBalance();
	}

}

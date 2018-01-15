package structural.decorator;

public class Account implements IAccount {

	private double balance;

	public Account(double balance) {
		this.balance = balance;
	}

	public void setBalance(double balance) {
		this.balance = balance;
	}

	@Override
	public void deposit(double balance) {

		this.balance = this.balance + balance;
	}

	@Override
	public void withdraw(double balance) {
		this.balance = this.balance - balance;
	}

	@Override
	public double getBalance() {
		// TODO Auto-generated method stub
		return balance;
	}

}

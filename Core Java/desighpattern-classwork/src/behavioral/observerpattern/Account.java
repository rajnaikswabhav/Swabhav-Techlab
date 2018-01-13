package behavioral.observerpattern;

import java.util.ArrayList;
import java.util.List;

public class Account {

	private int accountNumber;
	private String name;
	private double balnce;
	private List<IAccountListener> suscriberList = new ArrayList<IAccountListener>();;

	public Account(int accountNumber, String name, double balance) {
		this.accountNumber = accountNumber;
		this.name = name;
		this.balnce = balance;
	}

	public void addListener(IAccountListener accountListener) {

		suscriberList.add(accountListener);
	}

	public void deposite(double balance) {
		this.balnce = this.balnce + balance;
		notifyListener();
	}

	public void withdraw(double balance) {
		this.balnce = this.balnce - balance;
		notifyListener();
	}

	public void notifyListener() {
		for (IAccountListener aListener : suscriberList) {
			aListener.onBalanceChange(this);
		}
	}

	public void setBalnce(double balnce) {
		this.balnce = balnce;
	}

	public int getAccountNumber() {
		return accountNumber;
	}

	public String getName() {
		return name;
	}

	public double getBalnce() {
		return balnce;
	}

}

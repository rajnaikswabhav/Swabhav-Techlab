package behavioral.observerpattern;

public class SMSListener implements IAccountListener {

	@Override
	public void onBalanceChange(Account account) {

		System.out
				.println(account.getName()
						+ " you informed by SMS because your balance has been changed.Now your balance is "
						+ account.getBalnce() + " Ruppes..");
	}

}

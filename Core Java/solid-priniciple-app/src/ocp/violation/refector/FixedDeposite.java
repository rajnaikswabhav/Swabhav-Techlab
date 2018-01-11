package ocp.violation.refector;

public class FixedDeposite {

	private final String accountHolderName;
	private final double amount;
	private final int period;
	private final IFestivalType festival;

	public FixedDeposite(String accountHolderName, double amount, int period,
			IFestivalType festival) {

		this.accountHolderName = accountHolderName;
		this.amount = amount;
		this.period = period;
		this.festival = festival;
	}

	public String getAccountHolderName() {
		return accountHolderName;
	}

	public double getAmount() {
		return amount;
	}

	public int getPeriod() {
		return period;
	}

	public IFestivalType getFestival() {
		return festival;
	}

	public double calculateInterest() {

		double interestMoney = (amount * festival.getRate() / 100) * period;

		return interestMoney;

	}

}
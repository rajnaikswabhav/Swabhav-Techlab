package ocp.violation;


public class FixedDeposite {

	private final String accountHolderName;
	private final double amount;
	private final int period;
	private float interest;
	
	public FixedDeposite(String accountHolderName, double amount, int period,
			FestivalType festival) {

		this.accountHolderName = accountHolderName;
		this.amount = amount;
		this.period = period;
		
		if(festival == FestivalType.NORMAL)
		{
			interest = 7.0f;
		}
		
		else if(festival == FestivalType.DIWALI){
			interest = 7.5f;
		}
		
		else if(festival == FestivalType.NEWYEAR){
			 interest = 7.8f;
		}
	}

	public float getInterest() {
		return interest;
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

	public double calculateInterest() {

		double interestMoney = (amount * interest/ 100) * period;

		return interestMoney;

	}

}
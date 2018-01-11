package ocp.violation.ocp.test;

import ocp.violation.FestivalType;
import ocp.violation.FixedDeposite;

public class TestOCPViolation {

	public static void main(String[] args) {

		FixedDeposite deposite = new FixedDeposite("Akash", 10000, 2,
				FestivalType.NORMAL);
		FixedDeposite deposite2 = new FixedDeposite("xyz",20000,4,FestivalType.DIWALI);
		printDetails(deposite);
		printDetails(deposite2);
		/*System.out.println(deposite.getAccountHolderName()+" Your Interest on "+deposite.getAmount()+ " amount for  "
		+deposite.getPeriod()+ " years is: " + deposite.calculateInterest());
		System.out.println(deposite2.getAccountHolderName()+" Your Interest on "+deposite2.getAmount()+ " amount for "
				+deposite2.getPeriod()+ " years is: " + deposite2.calculateInterest());*/
	}
	
	public static void printDetails(FixedDeposite deposite)
	{
		String detail = deposite.getAccountHolderName();
		detail += " your amount is: " +deposite.getAmount();
		detail += " For "+deposite.getPeriod()+ " years";
		detail += " and your interest is: "+deposite.calculateInterest();
		detail += " with interest persentage: "+deposite.getInterest();
		System.out.println(detail);
	}

}

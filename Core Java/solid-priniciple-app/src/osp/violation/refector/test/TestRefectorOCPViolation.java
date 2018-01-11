package osp.violation.refector.test;

import ocp.violation.refector.DiwaliRate;
import ocp.violation.refector.FixedDeposite;
import ocp.violation.refector.NormalRate;

public class TestRefectorOCPViolation {

	public static void main(String[] args) {

		FixedDeposite deposite = new FixedDeposite("Akash", 10000, 2,
				new NormalRate());
		FixedDeposite deposite2 = new FixedDeposite("xyz", 20000, 4,
				new DiwaliRate());
		printDetails(deposite);
		printDetails(deposite2);
		/*
		 * System.out.println(deposite.getAccountHolderName()+" Your Interest on "
		 * +deposite.getAmount()+ " amount for  " +deposite.getPeriod()+
		 * " years is: " + deposite.calculateInterest());
		 * System.out.println(deposite2
		 * .getAccountHolderName()+" Your Interest on "+deposite2.getAmount()+
		 * " amount for " +deposite2.getPeriod()+ " years is: " +
		 * deposite2.calculateInterest());
		 */
	}

	public static void printDetails(FixedDeposite deposite) {
		String detail = deposite.getAccountHolderName();
		detail += " your amount is: " + deposite.getAmount();
		detail += " For " + deposite.getPeriod() + " years";
		detail += " and your interest is: " + deposite.calculateInterest();
		detail += " with interest persentage: "
				+ deposite.getFestival().getRate();
		System.out.println(detail);
	}

}

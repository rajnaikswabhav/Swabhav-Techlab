package dip.violation.refector.test;

import dip.violation.refector.TaxCalculator;
import dip.violation.refector.XMlLog;

public class TestRefectorDIPViolation {

	public static void main(String[] args) {

		TaxCalculator taxCalculate = new TaxCalculator(new XMlLog());
		System.out.println(taxCalculate.calculation(10,0));
	}

}

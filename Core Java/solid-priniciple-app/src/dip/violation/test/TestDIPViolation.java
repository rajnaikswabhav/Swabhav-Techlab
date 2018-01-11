package dip.violation.test;

import dip.violation.LogType;
import dip.violation.TaxCalculate;

public class TestDIPViolation {

	public static void main(String[] args) {

		TaxCalculate taxCalculate = new TaxCalculate(LogType.TXT);
		System.out.println(taxCalculate.calculation(10, 2));
	}

}

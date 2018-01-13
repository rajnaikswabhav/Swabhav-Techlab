package com.techlabs.factorymethod.test;

import com.techlabs.factorymethod.GetPlanFactory;
import com.techlabs.factorymethod.Plan;

public class TestFactoryMethod {

	public static void main(String[] args) {

		GetPlanFactory planFactory = new GetPlanFactory();
		Plan plan = planFactory.getPlan("commercialplan");
		
		plan.getRate();
		plan.calculateBill(10);
		
	}

}

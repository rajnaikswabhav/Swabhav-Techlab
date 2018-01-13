package creational.simplefactory.test;

import creational.simplefactory.AutoFactory;
import creational.simplefactory.CarType;
import creational.simplefactory.IAutomobile;

public class TestSimpleFactory {

	public static void main(String[] args) {

		AutoFactory autoFactory = AutoFactory.getInstance();

		IAutomobile car = autoFactory.make(CarType.TESLA);
		System.out.println(car.getClass());
		car.start();
		car.stop();
	}

}

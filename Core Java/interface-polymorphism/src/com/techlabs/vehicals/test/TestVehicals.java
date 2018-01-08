package com.techlabs.vehicals.test;

import com.techlabs.vehicals.Bike;
import com.techlabs.vehicals.Car;
import com.techlabs.vehicals.IMOveable;
import com.techlabs.vehicals.Truck;

public class TestVehicals {

	public static void main(String[] args) {
		Truck truck = new Truck();
		Car car = new Car();
		Bike bike = new Bike();

		IMOveable[] moveables = { truck, car, bike };
		startRace(moveables);
	}

	public static void startRace(IMOveable[] moveables) {
		System.out.println("Starting Race....");
		for (int i = 0; i < moveables.length; i++) {
			moveables[i].move();
		}
	}
}
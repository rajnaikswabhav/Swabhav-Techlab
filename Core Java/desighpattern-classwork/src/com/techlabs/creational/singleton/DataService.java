package com.techlabs.creational.singleton;

public class DataService {

	private static DataService instance;

	private DataService() {
		System.out.println("DataService created.....");
	}

	public static DataService getInstance() {
		if (instance == null) {
			instance = new DataService();
		}
		return instance;
	}

	public void doWork() {
		System.out.println("Doing Work " + this.hashCode());
	}
}

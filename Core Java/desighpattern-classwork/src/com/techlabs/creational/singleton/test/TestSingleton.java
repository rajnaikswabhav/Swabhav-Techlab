package com.techlabs.creational.singleton.test;

import com.techlabs.creational.singleton.DataService;

public class TestSingleton {

	public static void main(String[] args) {

		DataService service = DataService.getInstance();
		DataService service2 = DataService.getInstance();
		
		service.doWork();
		service2.doWork();
	}

}

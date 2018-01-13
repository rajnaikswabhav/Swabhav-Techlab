package com.techlabs.singleton;

public class EarlyInstantiationSingleton {

	private static EarlyInstantiationSingleton earlySingleton = new EarlyInstantiationSingleton();
	
	private EarlyInstantiationSingleton(){}
	
	public static EarlyInstantiationSingleton getInstance()
	{
		return earlySingleton;
	}
}

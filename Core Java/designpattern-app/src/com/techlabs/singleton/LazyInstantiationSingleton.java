package com.techlabs.singleton;

public class LazyInstantiationSingleton {

	private static LazyInstantiationSingleton lazySinglton ;
	
	private LazyInstantiationSingleton(){}
	
	public static LazyInstantiationSingleton getInstance()
	{
		lazySinglton = new LazyInstantiationSingleton();
		return lazySinglton;
	}
}

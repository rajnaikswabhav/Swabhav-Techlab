package com.techlabs.array;

public class SplitOverload {

	public static void main(String[] args) {
		String name = "https://www.swabhavtechlabs.com?name=akash";
		String[] names = name.split("\\.");
		System.out.println(names[1]);
		System.out.println();
		String[] names1 = name.split("\\.", 3);
		System.out.println(names1[1]);
	}
}

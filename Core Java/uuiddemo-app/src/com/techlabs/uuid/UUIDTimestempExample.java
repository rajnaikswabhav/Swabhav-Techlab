package com.techlabs.uuid;

import java.util.UUID;

public class UUIDTimestempExample {

	public static void main(String[] args) {

		UUID uuid =UUID.randomUUID();
		System.out.println("UUID : " + uuid);
		System.out.println("UUID Version : " + uuid.version());
		System.out.println("UUID varient :"+uuid.variant());
		System.out.println("Most Singificant Bite : "+uuid.getMostSignificantBits());
		System.out.println("Least Singificant Bite : "+uuid.getLeastSignificantBits());
	}

}

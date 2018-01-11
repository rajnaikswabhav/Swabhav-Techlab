package com.techlabs.uuid;

public class UUIDTimestempExample {

	public static void main(String[] args) {

		UUID uuid = Generators.timeBasedGenerator().generate();
		System.out.println("UUID : " + uuid);
		System.out.println("UUID Version : " + uuid.version());
		System.out.println("UUID Timestamp : " + uuid.timestamp());
	}

}

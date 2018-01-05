package com.techlabs.collection;

import java.util.*;

public class SetExample {

	public static void main(String[] args) {
		Set<String> set = new HashSet<String>();

		set.add("Akash");
		set.add("Mahavir");
		set.add("parth");
		set.add("Gaurang");
		set.add("Devang");
		set.add("Akash");
		set.remove("Akash");
		System.out.println(set.contains("Akash"));
		/*
		 * for(String str:set) { System.out.println(str); }
		 */

		Iterator<String> itr = set.iterator();
		while (itr.hasNext()) {
			System.out.println(itr.next());
		}

	}

}

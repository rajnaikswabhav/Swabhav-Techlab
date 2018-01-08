package com.techlabs.list;

import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.TreeSet;

public class TestSet {

	public static void main(String[] args) {

		Set<String> set = new HashSet<String>();
		//Set<String> set=new TreeSet<String>();
		//Set<String> set=new LinkedHashSet<String>();
		set.add("Akash");
		set.add("Brijesh");
		set.add("Parth");
		set.add("Mahavir");
		set.add("Devang");
		set.add("Carrom");
		set.add("Akash");
		print(set);
	}

	public static void print(Set<String> set) {
		for (String str : set) {
			System.out.println(str);
		}
	}

}

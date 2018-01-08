package com.techlabs.collection;

import java.util.*;

public class ListDemo {

	public static void main(String[] args) {
		List<String> list = new ArrayList<String>();

		list.add("Parth");
		list.add("akash");
		list.add("Mahavir");
		list.add("devang");
		list.add("Gaurang");
		list.add("Abram");
		Collections.sort(list);

		//list.add(2, "HIiiiiii...");
		
		Iterator<String> itr=list.iterator();
		while(itr.hasNext())
		{
			System.out.println(""+itr.next());
		}
		/*for (String str : list) {
			System.out.println(str);
		}*/

	}

}

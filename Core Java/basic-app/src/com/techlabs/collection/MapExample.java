package com.techlabs.collection;

import java.util.*;

public class MapExample {

	public static void main(String[] args) {
		Map<Integer, String> map = new HashMap<Integer, String>();
		map.put(101, "Akash");
		map.put(102, "Parth");
		map.put(103, "Devang");
		map.put(104, "Shreyash");
		map.put(105, "Mahavir");
		map.replace(101, "Gaurang"); // update
		map.remove(102); // delete

		Iterator itr=map.entrySet().iterator();
		while(itr.hasNext())
		{
			System.out.println(""+itr.next());
		}
		/*for (Map.Entry m : map.entrySet()) {
			System.out.println(m.getKey() + " " + m.getValue());
		}*/
	}

}

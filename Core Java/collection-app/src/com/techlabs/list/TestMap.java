package com.techlabs.list;

import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.TreeMap;

public class TestMap {

	public static void main(String[] args) {

		//Map<Integer, String> map=new HashMap<Integer, String>();
		//Map<Integer, String> map=new TreeMap<Integer, String>();
		Map<Integer, String> map=new LinkedHashMap<Integer, String>();
		map.put(101, "Akash");
		map.put(104, "Brijesh");
		map.put(103,"Parth");
		map.put(102, "Mahavir");
		map.put(105, "Devang");
		map.put(106, "Akash");
		map.put(101, "Akash");
		print(map);
	}

	public static void print(Map<Integer, String> map)
	{
		Iterator itr=map.entrySet().iterator();
		while(itr.hasNext())
		{
			System.out.println(itr.next());
		}
	}
}

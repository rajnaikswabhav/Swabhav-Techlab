package com.techlabs.list;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

public class TestList {

	public static void main(String[] args) {

		//List<String> list=new ArrayList<String>();
		List<String> list=new LinkedList<String>();
		list.add("Akash");
		list.add("Brijesh");
		list.add("Parth");
		list.add("Mahavir");
		list.add("Devang");
		list.add("Akash");
		print(list);
	}

	public static void print(List<String> list)
	{
		Iterator<String> itr=list.iterator();
		while(itr.hasNext())
		{
			System.out.println(itr.next());
		}
		/*for(String str:list)
		{
			System.out.println(str);
		}*/
	}
}

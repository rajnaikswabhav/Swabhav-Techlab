package com.techlabs.states;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.HashMap;

import java.util.Map;

public class StateService {

	Map<String,String> stateMap=new HashMap<String, String>();
	public void inIt()
	{
		try {
			FileReader reader=new FileReader("Data/cityDetail.csv");
			BufferedReader bufferedReader=new BufferedReader(reader);
			String detail;
			while((detail=bufferedReader.readLine()) != null)
			{
				String[] str=detail.split(",");
				stateMap.put(str[0], str[1]);
			}
			bufferedReader.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public String search(String code)
	{
		if(stateMap.get(code) != null)
		{
			return stateMap.get(code);
		}
		else{
			return "No Result Found";
		}
	}
}

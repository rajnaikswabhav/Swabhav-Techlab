package com.techlabs.states;

public class States {
	
	private String cityCode;
	private String cityName;
	
	public States(String cityCode,String cityName){
		this.cityCode=cityCode;
		this.cityName=cityName;
	}

	public String getCityCode() {
		return cityCode;
	}

	public String getCityName() {
		return cityName;
	}
	
}

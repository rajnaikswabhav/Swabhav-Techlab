package com.techlabs.database;

import com.techlabs.crudable.ICRUDable;

public class CustomerDatabase implements ICRUDable {

	@Override
	public void Create() {

		System.out.println("Customer Databse Created....");
	}

	@Override
	public void Select() {

		System.out.println("Customer Databse Selected....");
	}

	@Override
	public void Update() {

		System.out.println("Customer Databse Updated....");
	}

	@Override
	public void Delete() {

		System.out.println("Customer Databse Deleted....");
	}

}

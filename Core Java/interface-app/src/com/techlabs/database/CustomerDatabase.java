package com.techlabs.database;

import com.techlabs.crudable.CRUDable;

public class CustomerDatabase implements CRUDable {

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

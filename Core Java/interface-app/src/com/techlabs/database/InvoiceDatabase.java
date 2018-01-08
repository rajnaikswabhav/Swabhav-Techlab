package com.techlabs.database;

import com.techlabs.crudable.ICRUDable;

public class InvoiceDatabase implements ICRUDable {

	@Override
	public void Create() {

		System.out.println("Invoice Databse Created....");
	}

	@Override
	public void Select() {
		System.out.println("Invoice Databse Selected....");

	}

	@Override
	public void Update() {

		System.out.println("Invoice Databse Updated....");
	}

	@Override
	public void Delete() {

		System.out.println("Invoice Databse Deleted....");
	}

}

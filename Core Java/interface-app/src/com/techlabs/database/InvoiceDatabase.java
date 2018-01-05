package com.techlabs.database;

import com.techlabs.crudable.CRUDable;

public class InvoiceDatabase implements CRUDable {

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

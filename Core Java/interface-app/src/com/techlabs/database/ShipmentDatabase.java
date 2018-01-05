package com.techlabs.database;

import com.techlabs.crudable.CRUDable;

public class ShipmentDatabase implements CRUDable {

	@Override
	public void Create() {

		System.out.println("Shipment Databse Created....");
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

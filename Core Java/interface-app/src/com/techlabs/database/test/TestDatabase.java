package com.techlabs.database.test;

import com.techlabs.crudable.CRUDable;
import com.techlabs.database.CustomerDatabase;
import com.techlabs.database.InvoiceDatabase;
import com.techlabs.database.ShipmentDatabase;

public class TestDatabase {

	public static void main(String[] args) {

		DoDBOperation(new CustomerDatabase());
		DoDBOperation(new InvoiceDatabase());
		DoDBOperation(new ShipmentDatabase());
	}

	public static void DoDBOperation(CRUDable c) {
		c.Create();
		c.Select();
		c.Update();
		c.Delete();
		System.out.println();
	}

}

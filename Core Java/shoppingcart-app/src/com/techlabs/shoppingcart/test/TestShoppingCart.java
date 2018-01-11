package com.techlabs.shoppingcart.test;

import java.util.Date;
import com.techlabs.shoppingcart.Customer;
import com.techlabs.shoppingcart.LineItem;
import com.techlabs.shoppingcart.Order;
import com.techlabs.shoppingcart.Product;

public class TestShoppingCart {

	public static void main(String[] args) {
		Customer custmor =new Customer(101001,"Foo");
		Order order =new Order(101020,new Date());
		LineItem item =new LineItem(1010,5f,new Product(101, "Erasar",10,10f));
		LineItem item2 = new LineItem(1011,5,new Product(102,"Sharpener",20,10f));
		order.addItem(item);
		order.addItem(item2);
		custmor.addOrders(order);
		printInvoiceOf(custmor);
	}

	public static void printInvoiceOf(Customer customer) {

		String details = "Customer Id: " +customer.getId()+ "\n";
		details += "Custmor Name : " +customer.getName();
		System.out.println(details);
		for(Order o:customer.getOrders())
		{
			System.out.println("OrderList....");
			System.out.println("Id : " +o.getId());
			System.out.println("Order Date : " +o.getOrderDate());
			System.out.println("Checkout : " +o.calculateCheckout());
			
			for(LineItem item:o.getItems())
			{
				System.out.println("ItemList.....");
				System.out.println("Item Id : " +item.getId());
				System.out.println("Quantity is : " +item.getQuantity());
				System.out.println("FinalCost :" +item.costOfLineItem());
				System.out.println("Product id : " +item.getProduct().getId());
				System.out.println("ProductName : " +item.getProduct().getProductName());
				System.out.println("Product Cost : " +item.getProduct().getCost());
				System.out.println("Product Discount : "+item.getProduct().getDiscount());
				System.out.println("DiscountCost :" +item.getProduct().calculateDiscountCost());
			}
		}		
	}

}

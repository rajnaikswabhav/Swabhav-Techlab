package com.techlabs.collection;

public class LineItem {

	private int id;
	private String product;
	private int quantity;
	private int unitPrice;
	
	public LineItem(int id,String product,int quantity,int unitPrice)
	{
		this.id=id;
		this.product=product;
		this.quantity=quantity;
		this.unitPrice=unitPrice;
	}

	public int getId() {
		return id;
	}

	public String getProduct() {
		return product;
	}

	public int getQuantity() {
		return quantity;
	}

	public int getUnitPrice() {
		return unitPrice;
	}
	
	public double calculatePrice()
	{
		double price;
		price=quantity * unitPrice;
		return price;
	}
	
	@Override 
	public String toString()
	{
		return getId()+ "," +getProduct()+ "," +getQuantity()+ "," +getUnitPrice();
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + id;
		result = prime * result + ((product == null) ? 0 : product.hashCode());
		result = prime * result + quantity;
		result = prime * result + unitPrice;
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		LineItem other = (LineItem) obj;
		if (id != other.id)
			return false;
		if (product == null) {
			if (other.product != null)
				return false;
		} else if (!product.equals(other.product))
			return false;
		if (quantity == other.quantity)
			return false;
		if (unitPrice == other.unitPrice)
			return false;
		return true;
	}
}

package srp.violation;

public class Invoice {
	
	private final int id;
	private final String invoiceName;
	private final double cost;
	private final float discount;
	private static final float GST=18f;
	
	public Invoice(int id,String invoiceName,double cost,float discount) {

		this.id=id;
		this.invoiceName=invoiceName;
		this.cost=cost;
		this.discount=discount;
	}
	
	private double calculateDiscount()
	{
		double discountCost;
		discountCost = cost - (cost*discount/100);
		return discountCost;
	}
	
	private double calculateTax()
	{
		double tax;
		tax=cost*GST/100;
		return tax;
	}
	
	public double calculateTotal()
	{
		double total;
		total = calculateDiscount() + calculateTax();
		return total;
	}
	
	public void printInvoice()
	{
		System.out.println("InVoice Id: "+id);
		System.out.println("InvoiceName: "+invoiceName);
		System.out.println("Cost is: "+cost);
		System.out.println("Discount is: "+discount);
		System.out.println("DiscountCost: "+calculateDiscount());
		System.out.println("Tax is: "+calculateTax());
		System.out.println("Total is: "+calculateTotal());
	}
}

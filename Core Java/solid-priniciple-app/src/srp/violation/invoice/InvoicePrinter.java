package srp.violation.invoice;

public class InvoicePrinter {

	public void printer()
	{
		Invoice invoice = new Invoice(101, "Book", 20, 10f);
		printInvoice(invoice);
		
		Invoice invoice2 = new Invoice(102,"pen",5,10f);
		printInvoice(invoice2);
	}
	public void printInvoice(Invoice invoice) {

		System.out.println("InVoice Id: " + invoice.getId());
		System.out.println("InvoiceName: " + invoice.getInvoiceName());
		System.out.println("Cost is: " + invoice.getCost());
		System.out.println("Discount is: " + invoice.getDiscount());
		// System.out.println("DiscountCost: "+calculateDiscount());
		// System.out.println("Tax is: "+calculateTax());
		System.out.println("Total is: " + invoice.calculateTotal()+ "\n");
	}
}

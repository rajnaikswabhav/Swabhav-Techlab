package srp.violation.invoice;

public class Invoice {
	private final int id;
	private final String invoiceName;
	private final double cost;
	private final float discount;
	private static final float GST = 18f;

	public Invoice(int id, String invoiceName, double cost, float discount) {

		this.id = id;
		this.invoiceName = invoiceName;
		this.cost = cost;
		this.discount = discount;
	}

	private double calculateDiscount() {
		double discountCost;
		discountCost = cost - (cost * discount / 100);
		return discountCost;
	}

	private double calculateTax() {
		double tax;
		tax = cost * GST / 100;
		return tax;
	}

	public double calculateTotal() {
		double total;
		total = calculateDiscount() + calculateTax();
		return total;
	}

	public int getId() {
		return id;
	}

	public String getInvoiceName() {
		return invoiceName;
	}

	public double getCost() {
		return cost;
	}

	public float getDiscount() {
		return discount;
	}

	public static float getGst() {
		return GST;
	}

}

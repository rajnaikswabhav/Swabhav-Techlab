namespace ShoppingCartCore.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName  { get; set; }
        public double ProductCost  { get; set; }
        public float Discount { get; set; }
    }
}
namespace ShoppingCartCore.Model
{
    public class LineItem
    {
        public int ItemID { get; set; }
        public int Quantity  { get; set; }
        public Product Product  { get; set; }
    }
}
using ShoppingCartCore.Framework.Model;
using System;

namespace ShoppingCartCore.Model
{
    public class LineItem : Entity
    {
        public int Quantity { get; set; }
        public Guid orderId { get; set; }
        public Product Product { get; set; }
    }
}
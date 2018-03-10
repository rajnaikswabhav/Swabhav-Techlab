using ShoppingCartCore.Framework.Model;
using System;

namespace ShoppingCartCore.Model
{
    public class LineItem : Entity
    {
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
    }
}
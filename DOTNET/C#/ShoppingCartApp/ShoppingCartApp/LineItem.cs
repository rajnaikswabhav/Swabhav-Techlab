using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApp
{
    public class LineItem
    {
        private readonly int id;
        private readonly float quantity;
        private readonly Product product;
        private double finalCost;

        public LineItem(int id, float quantity, Product product)
        {
            this.id = id;
            this.quantity = quantity;
            this.product = product;
        }

        public double CostOfLineItem()
        {
            finalCost = product.CalculateDiscountCost() * quantity;
            return finalCost;
        }

        public Product Product { get { return product; } }
        public float Quantity { get { return quantity; } }
        public int Id { get { return id; } }
        public double FinalCost { get { return finalCost; } }
    }
}

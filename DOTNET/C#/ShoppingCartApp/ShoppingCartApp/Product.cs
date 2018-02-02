using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApp
{
    public class Product
    {
        private readonly int id;
        private readonly String productName;
        private double cost;
        private float discount;
        private double discountCost = 1;

        public Product(int id, String productName, double cost, float discount)
        {
            this.id = id;
            this.productName = productName;
            this.cost = cost;
            this.discount = discount;
        }

        public double CalculateDiscountCost()
        {
            discountCost = cost - (cost * discount) / 100;
            return discountCost;
        }

        public int Id { get { return id; } }
        public String ProductName { get { return productName; } }
        public double Cost { get { return cost; } }
        public float Discount { get { return discount; } }
        public double DiscounCost { get { return discountCost; } }
    }
}

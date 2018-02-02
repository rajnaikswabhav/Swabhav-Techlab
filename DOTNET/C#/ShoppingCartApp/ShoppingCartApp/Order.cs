using System;
using System.Collections.Generic;

namespace ShoppingCartApp
{
    public class Order
    {
        private readonly int id;
        private readonly DateTime orderDate;
        private List<LineItem> items = new List<LineItem>();
        private double checkoutCost = 0;

        public Order(int id, DateTime orderDate)
        {
            this.id = id;
            this.orderDate = orderDate;
        }

        public void AddItem(LineItem item)
        {
            items.Add(item);
        }

        public double CalculateCheckout()
        {
            foreach (LineItem item in items)
            {
                checkoutCost = checkoutCost + item.CostOfLineItem();
            }
            return checkoutCost;
        }

        public List<LineItem> Items { get { return items; } }
        public int Id { get { return id; } }
        public DateTime OrderDate { get { return orderDate; } }
        public double CheckoutCost { get { return checkoutCost; } }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApp
{
    class Customer
    {
        private readonly int id;
        private readonly String customerName;
        private List<Order> orders;

        public Customer(int id, String customerName)
        {
            this.id = id;
            this.customerName = customerName;
        }

        public void AddOrder(Order order)
        {
            orders = new List<Order>();
            orders.Add(order);
        }

        public List<Order> Orders { get { return orders; } }

        public int Id { get { return id; } }
        public String CustomerName { get { return customerName; } }
    }
}

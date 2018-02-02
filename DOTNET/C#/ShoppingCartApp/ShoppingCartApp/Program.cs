using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer(101, "Akash");
            Order order = new Order(101012, new DateTime());
            LineItem item = new LineItem(1234, 3, new Product(102030, "Erasar", 10, 10));
            LineItem item2 = new LineItem(1243, 5, new Product(102033, "Sharpner", 20, 10));
            order.AddItem(item);
            order.AddItem(item2);
            customer.AddOrder(order);
            PrintInvoiceOf(customer);
        }

        public static void PrintInvoiceOf(Customer customer)
        {
            String details = "Customer Id: " + customer.Id + "\n";
            details += "Customer Name: " + customer.CustomerName + "\n";
            Console.WriteLine(details);

            foreach (var order in customer.Orders)
            {
                Console.WriteLine("Order List......");
                Console.WriteLine("Id: " + order.Id);
                Console.WriteLine("Order Date: " + order.OrderDate);
                Console.WriteLine("Checkout: " + order.CalculateCheckout());

                foreach (var item in order.Items)
                {
                    Console.WriteLine("Item List.....");
                    Console.WriteLine("Item Id: " + item.Id);
                    Console.WriteLine("Quantity is: " + item.Quantity);
                    Console.WriteLine("FinalCost is: " + item.CostOfLineItem());
                    Console.WriteLine("Product Details....");
                    Console.WriteLine("Product id: " + item.Product.Id);
                    Console.WriteLine("Product Name: " + item.Product.ProductName);
                    Console.WriteLine("Product Cost: " + item.Product.Cost);
                    Console.WriteLine("Product Discount: " + item.Product.Discount);
                    Console.WriteLine("Product DiscountCost: " + item.Product.CalculateDiscountCost());
                }
            }

        }
    }
}

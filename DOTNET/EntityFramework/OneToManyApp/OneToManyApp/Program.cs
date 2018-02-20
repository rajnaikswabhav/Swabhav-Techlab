using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneToManyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();
            customer.Id = 567;
            customer.Name = "Akash";

            Order order = new Order();
            order.OrderId = 56;
            order.OrderCost = 50000;
            order.OrderItem = "Electronics";
            order.CustomerId = customer.Id;

            LineItem item = new LineItem();
            item.ID = 1111;
            item.ItemName = "Plasma TV";
            item.ItemPrice = 20000;
            item.product = new Product { ProductId = 1,ProductName = "Plasma TV"};
            item.OrderId = order.OrderId;

            LineItem item2 = new LineItem();
            item2.ID = 1112;
            item2.ItemName = "LED TV";
            item2.ItemPrice = 30000;
            item2.product = new Product { ProductId = 2, ProductName = "Led TV" };
            item2.OrderId = order.OrderId;

            order.items.Add(item);
            order.items.Add(item2);

            Order order2 = new Order();
            order2.OrderId = 78;
            order2.OrderCost = 60000;
            order2.OrderItem = "Auto Parts";
            order2.CustomerId = customer.Id;

            LineItem item3 = new LineItem();
            item3.ID = 1123;
            item3.ItemName = "Car Fender";
            item3.ItemPrice = 60000;
            item3.product = new Product { ProductId = 3, ProductName = "Car Fender" };
            item3.OrderId = order2.OrderId;

            order2.items.Add(item3);

            Order order3 = new Order();
            order3.OrderId = 91;
            order3.OrderCost = 100000;
            order3.OrderItem = "Computer";
            order3.CustomerId = customer.Id;

            LineItem item4 = new LineItem();
            item4.ID = 1134;
            item4.ItemName = "LapTop";
            item4.ItemPrice = 40000;
            item4.product = new Product { ProductId = 4, ProductName = "LapTop" };
            item4.OrderId = order3.OrderId;

            LineItem item5 = new LineItem();
            item5.ID = 1135;
            item5.ItemName = "Mac Book";
            item5.ItemPrice = 60000;
            item5.product = new Product { ProductId = 5, ProductName = "Mac Book" };
            item5.OrderId = order3.OrderId;

            order3.items.Add(item4);
            order3.items.Add(item5);

            customer.orders.Add(order);
            customer.orders.Add(order2);
            customer.orders.Add(order3);

            Customer customer2 = new Customer();
            customer2.Id = 789;
            customer2.Name = "Parth";

            Order order4 = new Order();
            order4.OrderId = 34;
            order4.OrderCost = 45000;
            order4.OrderItem = "Fitness";
            order4.CustomerId = customer2.Id;

            LineItem item6 = new LineItem();
            item6.ID = 1146;
            item6.ItemName = "TreadMill";
            item6.ItemPrice = 30000;
            item6.product = new Product { ProductId = 6, ProductName = "TreadMill" };
            item6.OrderId = order4.OrderId;

            LineItem item7 = new LineItem();
            item7.ID = 1147;
            item7.ItemName = "Weights";
            item7.ItemPrice = 10000;
            item7.product = new Product { ProductId = 7, ProductName = "Weights" };
            item7.OrderId = order4.OrderId;

            LineItem item8 = new LineItem();
            item8.ID = 1148;
            item8.ItemName = "Track";
            item8.ItemPrice = 5000;
            item8.product = new Product { ProductId = 8, ProductName = "Track" };
            item8.OrderId = order4.OrderId;

            order4.items.Add(item6);
            order4.items.Add(item7);
            order4.items.Add(item8);

            Order order5 = new Order();
            order5.OrderId = 67;
            order5.OrderCost = 50000;
            order5.OrderItem = "Sports";
            order5.CustomerId = customer2.Id;

            LineItem item9 = new LineItem();
            item9.ID = 1159;
            item9.ItemName = "Dirt Bike";
            item9.ItemPrice = 50000;
            item9.product = new Product { ProductId = 9, ProductName = "Dirt Bike" };
            item9.OrderId = order5.OrderId;

            order5.items.Add(item9);

            customer2.orders.Add(order4);
            customer2.orders.Add(order5);

            OneToManyDbContext context = new OneToManyDbContext();
            context.Customers.Add(customer);
            context.Customers.Add(customer2);

            context.Order.Add(order);
            context.Order.Add(order2);
            context.Order.Add(order3);
            context.Order.Add(order4);
            context.Order.Add(order5);

            context.LineItems.Add(item);
            context.LineItems.Add(item2);
            context.LineItems.Add(item3);
            context.LineItems.Add(item4);
            context.LineItems.Add(item5);
            context.LineItems.Add(item6);
            context.LineItems.Add(item7);
            context.LineItems.Add(item8);
            context.LineItems.Add(item9);

            context.SaveChanges();
        }
    }
}

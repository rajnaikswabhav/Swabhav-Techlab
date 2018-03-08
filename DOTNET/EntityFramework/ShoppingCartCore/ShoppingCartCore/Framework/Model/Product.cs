using ShoppingCartCore.Framework.Model;
using System;

namespace ShoppingCartCore.Model
{
    public class Product : Entity
    {
        public string ProductName  { get; set; }
        public string ProductCatagory  { get; set; }
        public double ProductCost  { get; set; }
        public float Discount { get; set; }
    }
}
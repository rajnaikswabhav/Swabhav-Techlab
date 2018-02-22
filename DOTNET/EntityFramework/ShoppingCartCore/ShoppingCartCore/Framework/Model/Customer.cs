using ShoppingCartCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string City  { get; set; }
        public List<Order> Orders { get; set; }
    }
}

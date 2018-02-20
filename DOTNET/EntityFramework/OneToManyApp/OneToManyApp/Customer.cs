using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneToManyApp
{
    public class Customer
    {
        public List<Order> orders = new List<Order>();
        public  int Id { get; set; }
        public string Name  { get; set; }
        public List<Order> Orders  { get; set; }
    }
}

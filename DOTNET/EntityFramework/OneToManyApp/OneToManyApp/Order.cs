using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneToManyApp
{
    public class Order
    {
        public List<LineItem> items = new List<LineItem>();
        public int OrderId { get; set; }
        public string OrderItem { get; set; }
        public double OrderCost { get; set; }
        public int CustomerId { get; set; };
    }
}

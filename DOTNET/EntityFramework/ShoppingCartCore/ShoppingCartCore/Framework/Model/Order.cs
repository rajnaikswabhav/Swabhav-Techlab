using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Model
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate  { get; set; }
        public List<LineItem> Items  { get; set; }
    }
}

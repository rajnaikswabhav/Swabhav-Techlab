using ShoppingCartCore.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Model
{
    public class Order : Entity
    {
        public DateTime OrderDate { get; set; }; 
        public Guid userId { get; set; }
        public List<LineItem> items = new List<LineItem>();
    }
}

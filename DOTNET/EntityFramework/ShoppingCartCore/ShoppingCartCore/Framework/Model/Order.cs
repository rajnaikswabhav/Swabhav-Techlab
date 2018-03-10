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
        public Order(DateTime date,Guid id,string status) {
            OrderDate = date;
            UserId = id;
            Status = status;
        }

        public DateTime OrderDate { get; set; } 
        public Guid UserId { get; set; }
        public string Status  { get; set; }
    }
}

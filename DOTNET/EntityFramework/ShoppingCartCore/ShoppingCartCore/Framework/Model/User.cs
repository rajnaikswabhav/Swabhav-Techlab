using ShoppingCartCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Model
{
    public class User : Entity
    {
        public string FirstName  { get; set; }
        public string LastName  { get; set; }
        public int PhoneNo  { get; set; }
        public string Email  { get; set; }
        public string Password  { get; set; }

        public List<Order> orders = new List<Order>();
    }
}

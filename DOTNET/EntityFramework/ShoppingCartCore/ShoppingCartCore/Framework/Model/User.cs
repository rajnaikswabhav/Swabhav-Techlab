using ShoppingCartCore.Framework.Enums;
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
        public int Age { get; set; }
        public Gender Gender  { get; set; }
        public UserRole Role  { get; set; }
        public string Email  { get; set; }
        public string Password  { get; set; }
        public string ProfilePhoto { get; set; }

        public List<Address> addreses = new List<Address>();
        public List<Order> orders = new List<Order>();
    }
}

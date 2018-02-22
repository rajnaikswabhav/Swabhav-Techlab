using ShoppingCartCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName  { get; set; }
        public string Password  { get; set; }
        public Role Role  { get; set; }

        public User() { }


    }
}

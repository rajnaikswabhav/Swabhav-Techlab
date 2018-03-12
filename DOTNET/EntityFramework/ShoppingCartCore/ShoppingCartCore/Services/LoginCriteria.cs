using ShoppingCartCore.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Services
{
    public class LoginCriteria
    {
        public string UserName  { get; set; }
        public string Password  { get; set; }
        public string Role  { get; set; }
    }
}

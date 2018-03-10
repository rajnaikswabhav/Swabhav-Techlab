using ShoppingCartCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Model
{
    public class Wishlist : Entity
    {
        public Guid UserId { get; set; }
    }
}

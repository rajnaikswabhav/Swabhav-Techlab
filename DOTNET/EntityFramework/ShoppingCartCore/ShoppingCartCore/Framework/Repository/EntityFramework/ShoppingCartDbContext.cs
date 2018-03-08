using ShoppingCartCore.Framework.Model;
using ShoppingCartCore.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Repository.EntityFramework
{
    class ShoppingCartDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Order> Order  { get; set; }
        public DbSet<LineItem> LineItem  { get; set; }
        public DbSet<Wishlist> Wishlist  { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}

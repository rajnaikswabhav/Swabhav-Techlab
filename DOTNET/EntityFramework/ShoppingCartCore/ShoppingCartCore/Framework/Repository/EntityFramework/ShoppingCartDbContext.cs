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
    public class ShoppingCartDbContext : DbContext
    {

        public ShoppingCartDbContext() : base("ShoppingCartDbContext")
        {
            Database.SetInitializer<ShoppingCartDbContext>(null);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Role  { get; set; }
    }
}

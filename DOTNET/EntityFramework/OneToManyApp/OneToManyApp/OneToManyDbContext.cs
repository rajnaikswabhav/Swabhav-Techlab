using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneToManyApp
{
    public class OneToManyDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order>  Order{ get; set; }
        public DbSet<LineItem> LineItems  { get; set; }
    }
}

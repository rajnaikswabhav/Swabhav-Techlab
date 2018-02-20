using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ConsoleEntityApp
{
    public class HiTechDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public HiTechDbContext()
        {
          //  this.Database.Log = Logger.Log;
        }

        
    }
}

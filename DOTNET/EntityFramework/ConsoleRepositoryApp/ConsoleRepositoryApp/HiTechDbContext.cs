using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRepositoryApp
{
    public class HiTechDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public HiTechDbContext()
        {
            this.Database.Log = Logger.Log;
        }
    }
}

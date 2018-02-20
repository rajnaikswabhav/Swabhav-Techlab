using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEFApp
{
    public class EmpDbContext : DbContext
    {
        public  DbSet<Department> DEPT  { get; set; }
        public DbSet<Employee>  EMP { get; set; }
    }
}

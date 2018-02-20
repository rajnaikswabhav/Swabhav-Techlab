using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEFApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Department department = new Department();
            Employee employee = new Employee();
            EmpDbContext context = new EmpDbContext();
            context.EMP.Add(employee);
            context.DEPT.Add(department);
            context.SaveChanges();
        }
    }
}

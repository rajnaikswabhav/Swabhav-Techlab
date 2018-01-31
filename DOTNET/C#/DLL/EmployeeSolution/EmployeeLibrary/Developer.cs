using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeLibrary
{
   public class Developer : Employee
    {
        public override double CalculateSalary()
        {
            return BasicSalary + Performance ;
        }
    }
}

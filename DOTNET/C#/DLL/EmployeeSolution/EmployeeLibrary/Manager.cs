using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeLibrary
{
    public class Manager : Employee
    {
        public override double CalculateSalary()
        {
            double salary = BasicSalary + HouseRentAllowance + DernessAllowance;
            return salary;
        }
    }
}

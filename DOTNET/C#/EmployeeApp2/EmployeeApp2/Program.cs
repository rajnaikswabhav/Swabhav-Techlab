using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeService employeeService = new EmployeeService();
            employeeService.IntializeList();
            HashSet<Employee> employeeList = employeeService.get();
            Console.WriteLine(employeeList.Count);
            foreach (var employee in employeeList)
            {
                Console.WriteLine(employee);
            }
        }
    }
}

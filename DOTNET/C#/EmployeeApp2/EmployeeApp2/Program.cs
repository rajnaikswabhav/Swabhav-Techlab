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
        }

        public static void SortByName(HashSet<Employee> emSet)
        {
            Console.WriteLine("Sort By Name....");
            HashSet<Employee> sortedByName = new HashSet<Employee>();
            
        }
    }
}

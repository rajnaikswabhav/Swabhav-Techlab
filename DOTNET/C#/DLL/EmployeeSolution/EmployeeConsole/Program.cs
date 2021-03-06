﻿using EmployeeLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new Manager
            {
                EmployeeName = "Akash",
                DateOfBirth = new DateTime(1996, 5, 15),
                BasicSalary = 15000,
            };
            PrintSalarySlip(manager);
            var developer = new Developer
            {
                EmployeeName = "Brijesh",
                DateOfBirth = new DateTime(1995, 2, 15),
                BasicSalary = 10000
            };
            PrintSalarySlip(developer);
        }

        public static void PrintSalarySlip(Employee employee)
        {
            Console.WriteLine("Employee Name is: " + employee.EmployeeName);
            Console.WriteLine("Employee Degignation is: " + employee.GetType().Name);
            Console.WriteLine("Employee Basic Salary is: "+employee.BasicSalary);
            if (typeof(Manager).Equals(employee.GetType()) )
            {
                Console.WriteLine("Employee HouseRentAllowance is: " + employee.HouseRentAllowance);
                Console.WriteLine("Employee DearnessAllowance is: " + employee.DernessAllowance);
            }
            else if (typeof(Developer).Equals(employee.GetType()))
            {
                Console.WriteLine("Employee Performance is: " + employee.Performance);
            }
            Console.WriteLine("Employee BirthDate is: " + employee.DateOfBirth);
            Console.WriteLine("Employee Age is: " + employee.CalculateAge());
            Console.WriteLine("Employee CTC Salary is : " + employee.CalculateSalary());
            Console.WriteLine();
        }
    }
}

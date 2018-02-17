using EmployeeDropdownApp.Models;
using EmployeeMVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDropdownApp.Services
{
    public class EmployeeService
    {
        private static List<Employee> listOfEmployee = new List<Employee>();

        private static List<Department> listOfDept = new List<Department> {
            new Department {DeptNo=10 , DName="Account" },
            new Department {DeptNo = 20 , DName = "Sales" },
            new Department {DeptNo = 30 , DName = "Clark"},
            new Department {DeptNo = 40 , DName = "Operation" }
        };

        static EmployeeService()
        {
            listOfEmployee.Add(new Employee(101, "Akash", 10000, 10));
            listOfEmployee.Add(new Employee(102, "Brijesh", 15000, 20));
            listOfEmployee.Add(new Employee(103, "Parth", 20000, 30));
            listOfEmployee.Add(new Employee(104, "Mahavir", 25000, 30));
            listOfEmployee.Add(new Employee(105, "Gaurang", 30000, 10));
            listOfEmployee.Add(new Employee(106, "Shreyash", 15000, 20));
            listOfEmployee.Add(new Employee(107, "Devang", 10000, 10));
        }

        public List<SelectListItem> GetNames()
        {
            
            var selectItem = new List<SelectListItem>();
            foreach (var name in listOfDept)
            {
                selectItem.Add(new SelectListItem
                {
                    Value = name.DeptNo.ToString(),
                    Text = name.DName
                });
            }
            return selectItem;
        }

        public List<Employee> GetEmployees(string deptNo)
        {
            var empList = listOfEmployee.Where(e => e.DeptNo == int.Parse(deptNo)).ToList();
            return empList;
        }

    }
}
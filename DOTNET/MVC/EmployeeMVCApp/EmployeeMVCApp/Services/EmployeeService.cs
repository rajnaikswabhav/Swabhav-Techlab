using EmployeeMVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeMVCApp.Services
{
    public class EmployeeService
    {
        private static List<Employee> listOfEmployee = new List<Employee>();

        static EmployeeService()
        {
            Employee emp = new Employee(101, "Akash", 10000, 10);
            listOfEmployee.Add(emp);
            Employee emp2 = new Employee(102, "Brijesh", 15000, 20);
            listOfEmployee.Add(emp2);
            Employee emp3 = new Employee(103, "Parth", 20000, 30);
            listOfEmployee.Add(emp3);
        }

        public List<Employee> GetEmp()
        {
            return listOfEmployee;
        }

        public Employee Get(int id)
        {
           var emp = listOfEmployee.Where(e => e.Id == id).Single();
            return emp;
        }

        public void Update(Employee emp)
        {
            var emp1 = listOfEmployee.Where(e => e.Id == emp.Id).Single();
            listOfEmployee.Remove(emp1);
            listOfEmployee.Add(emp);
        }

        public void Add(Employee emp)
        {
            listOfEmployee.Add(emp);
        }

        public void Delete(int? id)
        {
            var emp = listOfEmployee.Where(e => e.Id == id).Single();
            listOfEmployee.Remove(emp);
        }
    }
}
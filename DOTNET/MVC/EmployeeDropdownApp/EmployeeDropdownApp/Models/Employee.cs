using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeMVCApp.Models
{
    public class Employee
    {
        private int? id;
        private string name;
        private int salary;
        private int deptNo;

        public Employee() { }

        public Employee(int? id, string name, int salary, int deptNo)
        {
            this.id = id;
            this.name = name;
            this.salary = salary;
            this.deptNo = deptNo;
        }

        public int? Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }
        public int DeptNo { get { return deptNo; } set { deptNo = value; } }

    }
}
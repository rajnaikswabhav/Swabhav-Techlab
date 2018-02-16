using EmployeeMVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeMVCApp.ViewModel
{
    public class IndexViewModel
    {
     
        public List<Employee> Employees { get; set; }
        public int EmpCount { get; set; }
    }
}
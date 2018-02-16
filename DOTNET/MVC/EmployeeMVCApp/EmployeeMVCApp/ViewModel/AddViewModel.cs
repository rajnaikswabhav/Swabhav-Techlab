using EmployeeMVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeMVCApp.ViewModel
{
    public class AddViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public int DeptNo { get; set; }
    }
}
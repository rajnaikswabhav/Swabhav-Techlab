using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDropdownApp.ViewModel
{
    public class DeptViewModel
    {
        public IEnumerable<SelectListItem> DNames { get; set; }
    }
}
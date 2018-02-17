using EmployeeDropdownApp.Services;
using EmployeeDropdownApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDropdownApp.Controllers
{
    public class HomeController : Controller
    {
        private EmployeeService empService = new EmployeeService();

        public ActionResult Index()
        {
            var dvm = new DeptViewModel();
            dvm.DNames = empService.GetNames() ;
            return View(dvm);
        }

        [HttpPost]
        public ActionResult Index(string deptNo)
        {
            var employees = empService.GetEmployees(deptNo);
            return Json(employees , JsonRequestBehavior.AllowGet);
        }
    }
}
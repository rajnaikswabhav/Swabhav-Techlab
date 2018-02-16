using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewBagAndDataApp.Models;

namespace ViewBagAndDataApp.Controllers
{
    public class EmployeeServiceController : Controller
    {
        public ActionResult GetEmpDetails()
        {
            Employee employee = new Employee();
            employee.Name = "Akash";
            employee.Age = 21;
            employee.Email = "akash@akash.com";
            employee.City = "Ahmedabad";
            //ViewData["EmployeeDetails"] = employee;
            ViewBag.EmployeeDetails = employee;
            return View("EmployeeView");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataAnnotstionApp.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentDetails(Models.Student student)
        {
            if (ModelState.IsValid)
            {
                ViewBag.id = student.Id;
                ViewBag.name = student.Name;
                ViewBag.age = student.Age;
                ViewBag.userName = student.UserName;
                ViewBag.pass = student.Password;
                ViewBag.email = student.Email;
                return View("Index");
            }
            else {
                ViewBag.id = "No Data";
                ViewBag.name = "No Data" ;
                ViewBag.age = "No Data" ;
                ViewBag.userName = "No Data" ;
                ViewBag.pass = "No Data" ;
                ViewBag.email = "No Data" ;
                return View("Index");
            }
        }
    }
}
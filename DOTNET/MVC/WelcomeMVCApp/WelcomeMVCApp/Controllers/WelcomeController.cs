using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WelcomeMVCApp.Controllers
{
    public class WelcomeController : Controller
    {
        public String SayHello()
        {
            return "<br><h1>Hello Says MVC Action Method</h1>";
        }

        public ActionResult getDept(Nullable<int> id)
        {
            return Content("<h1>Display Employees in department "+id+"</h1>");
        }

        public ActionResult getEmp(Nullable<int> rollNo)
        {
            return Content("<h1>Display Employee of RollNo "+rollNo+"</h1>");
        }
         
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var list =
                new []{
                   new { id = 101,name = "Akash" },
                   new { id = 102 , name = "Brijesh"},
                   new {id = 103 , name = "Parth"}
                };

            return Json(list,JsonRequestBehavior.AllowGet);
        }
    }
}
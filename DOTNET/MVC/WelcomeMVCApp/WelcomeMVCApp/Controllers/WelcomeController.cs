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

        public ActionResult getDept(int id)
        {
            return Content("<h1>Display Employees in department "+id+"</h1>");
        }

        public ActionResult getEmp(int rollNo)
        {
            return Content("<h1>Display Employee of RollNo "+rollNo+"</h1>");
        }
    }
}
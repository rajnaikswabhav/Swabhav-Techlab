using CustomFilterApp.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomFilterApp.Controllers
{
    [CustomFilter]
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            
            return View();
        }
    }
}
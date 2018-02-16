using EmployeeMVCApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeMVCApp.Controllers
{
    public class SessionController : Controller
    {
        public ActionResult Index()
        {
            var ivm1 = new IndexViewModel2();

            if (Session["counter"] == null)
            {
                Session["counter"] = 1;
            }
            
                ivm1.OldValue = Convert.ToInt32(Session["counter"]);
                ivm1.NewValue = Convert.ToInt32(Session["counter"]) + 1;
            
            

            return View(ivm1);
        }
    }
}
using EmployeeMVCApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeMVCApp.Controllers
{
    public class ApplicationController : Controller
    {
        public ActionResult Index()
        {
            var ivm = new IndexViewModel2();
            if(System.Web.HttpContext.Current.Application["counter"] == null)
            {
                System.Web.HttpContext.Current.Application["counter"] = 1; 
            }
            ivm.OldValue = int.Parse(Session["counter"].ToString());
            ivm.NewValue = int.Parse(Session["counter"].ToString()) + 1;
            return View(ivm);
        }
    }
}
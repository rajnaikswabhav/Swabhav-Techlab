using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomFilterApp.Filters
{
    public class CustomFilter : ActionFilterAttribute,IActionFilter
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.ResultExcuting = "Result Excuting..";
            base.OnResultExecuting(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.RouteData.Values["HomeController"];
            var actionName = filterContext.RouteData.Values["Index"];
            filterContext.Controller.ViewBag.ActionExcuting = "Action Excuting...";
            base.OnActionExecuting(filterContext);
        }
    }
}
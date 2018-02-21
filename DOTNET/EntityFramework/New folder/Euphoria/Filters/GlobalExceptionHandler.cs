using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;

namespace Techlabs.Euphoria.API.Filters
{
    public class GlobalExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ValidationException)
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            else
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WelcomeApiApp.Controllers
{
    public class WelcomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok("Get Method Invoked.");
        }

        public IHttpActionResult Post()
        {
            return InternalServerError();
        }

        public IHttpActionResult Put() {
            return Ok("Put Method Invoked.");
        }

        public IHttpActionResult Delete()
        {
            return Ok("Delete Method Invoked.");
        }
    }
}
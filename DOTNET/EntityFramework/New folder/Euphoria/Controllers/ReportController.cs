using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Techlabs.Euphoria.API.Controllers
{
    [RoutePrefix("api/v1/organizers/{organizerId:guid}/reports/{venueId:guid}")]
    public class ReportController : ApiController
    {
        //public IHttpActionResult GetTicketsBooked() {
        //    return Ok();
        //}


        //public IHttpActionResult GetTicketsIssued()
        //{
        //    return Ok();
        //}


        //[Route("visitors/{registrationType}")]
        //public IHttpActionResult GetRegisteredVisitors()
        //{
        //    return Ok();
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ActiveEventDTO
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; }
        public string City { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Address { get; set; }
    }
}
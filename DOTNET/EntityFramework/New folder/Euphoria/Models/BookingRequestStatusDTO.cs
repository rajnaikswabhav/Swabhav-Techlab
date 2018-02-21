using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class BookingRequestStatusDTO
    {
        public string Status { get; set; }
        public string Action { get; set; }
        public double FinalAmount { get; set; }
        public string Comment { get; set; }
        public int? DiscountPercent { get; set; }
        public int? DiscountAmount { get; set; }
    }
}
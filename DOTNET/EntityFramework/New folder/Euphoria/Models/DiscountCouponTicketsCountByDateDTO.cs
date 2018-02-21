using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class DiscountCouponTicketsCountByDateDTO
    {
        public string Day { get; set; }
        public string Date { get; set; }
        public int VisitorCount { get; set; }
        public int TotalTicketCount { get; set; }
    }
}
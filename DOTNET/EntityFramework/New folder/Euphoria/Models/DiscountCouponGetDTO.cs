using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class DiscountCouponGetDTO
    {
        public string CouponCode { get; set; }
        public int TotalAmount { get; set; }
        public int NumberOfTickets { get; set; }
    }
}
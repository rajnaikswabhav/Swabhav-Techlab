using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class DiscountCouponDTO
    {
        public Guid DiscountCouponId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CouponCode { get; set; }
        public int Discount { get; set; }
        public string DiscountText { get; set; }
        public bool IsActive { get; set; }
        public string DiscountType { get; set; }
        public int TicketCount { get; set; }
        public int VisitorCount { get; set; }
    }
}
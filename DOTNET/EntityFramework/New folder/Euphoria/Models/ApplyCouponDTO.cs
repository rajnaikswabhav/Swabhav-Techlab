using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ApplyCouponDTO
    {
        public Guid DiscountId { get; set; }
        public int DiscountedAmount { get; set; }
        public string DiscountText { get; set; }
    }
}
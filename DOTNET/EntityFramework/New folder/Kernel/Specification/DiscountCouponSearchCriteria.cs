using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class DiscountCouponSearchCriteria
    {
        public Guid EventId { get; set; }
        public string CouponCode { get; set; }
        public DateTime Date { get; set; }
    }
}

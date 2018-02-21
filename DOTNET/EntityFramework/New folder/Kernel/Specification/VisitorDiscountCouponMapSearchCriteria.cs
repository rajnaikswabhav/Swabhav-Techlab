using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class VisitorDiscountCouponMapSearchCriteria
    {
        public Guid VisitorId { get; set; }
        public Guid DiscountCouponId { get; set; }
        public Guid EventTicketId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

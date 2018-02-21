using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class VisitorBookingExhibitorDiscountSearchCriteria
    {
        public Guid VisitorId { get; set; }
        public Guid EventId { get; set; }
        public Guid BookingExhibitorDiscountId { get; set; }
    }
}

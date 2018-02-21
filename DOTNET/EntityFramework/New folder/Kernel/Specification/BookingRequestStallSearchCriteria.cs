using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class BookingRequestStallSearchCriteria
    {
        public Guid EventId { get; set; }
        public Guid BookingRequestId { get; set; }
        public string Status { get; set; }
    }
}

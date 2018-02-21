using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class BookingRequestSearchCriteria
    {
        public Guid EventId { get; set; }
        public Guid ExhibitorId { get; set; }
        public string Status { get; set; }
        public string ExhibitorName { get; set; }
        public string BookingRequestId { get; set; }
    }
}

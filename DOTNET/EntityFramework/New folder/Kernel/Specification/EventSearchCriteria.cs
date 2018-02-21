using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class EventSearchCriteria
    {
        public Guid EventId { get; set; }
        public Guid VenueId { get; set; }
        public bool IsTicketingActive { get; set; }
        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class EventTicketReportSearchCriteria
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDateForSearch { get; set; }
        public DateTime EndDateForSearch { get; set; }
        public Guid EventId { get; set; }
        public Guid adminId { get; set; }
        public DateTime SingleDate { get; set; }
        public int Pincode { get; set; }
        public bool? IsMobile { get; set; }
        public bool? IsWeb { get; set; }
        public bool? IsPayatLocation { get; set; }
        public bool? IsPayOnline { get; set; }
        public bool? PaymentCompleted { get; set; }
    }
}

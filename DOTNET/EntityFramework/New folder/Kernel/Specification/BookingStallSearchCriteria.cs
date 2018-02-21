using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class BookingStallSearchCriteria
    {
        public Guid EventId { get; set; }
        public string Status { get; set; }
        public Guid BookingId { get; set; }
        public Guid StallId { get; set; }
        public Guid SalesPerson { get; set; }
        public Guid CountryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

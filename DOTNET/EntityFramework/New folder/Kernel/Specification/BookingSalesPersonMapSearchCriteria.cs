using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class BookingSalesPersonMapSearchCriteria
    {
        public Guid BookingId { get; set; }
        public Guid SalesPersonId { get; set; }
        public Guid EventId { get; set; }
        public string ExhibitorName { get; set; }

        public Guid PavilionId { get; set; }
        public Guid ExhibitorIndustryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid PartnerId { get; set; }
    }
}

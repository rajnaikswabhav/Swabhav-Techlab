using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class BookingSearchCriteria
    {
        public string CompanyName { get; set; }
        public string Pavilion { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Category { get; set; }
        public Guid EventId { get; set; }
        public Guid ExhibitorId { get; set; }
        public Guid StallId { get; set; }
        public Guid CountryId { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BookingReqestId { get; set; }

        public Guid SalesPersonId { get; set; }
        public Guid PavilionId { get; set; }
        public Guid ExhibitorIndustryId { get; set; }
    }
}

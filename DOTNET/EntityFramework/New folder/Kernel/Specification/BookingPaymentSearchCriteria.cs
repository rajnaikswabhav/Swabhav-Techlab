using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class BookingPaymentSearchCriteria
    {
        public Guid SalesPersonId { get; set; }
        public Guid BookingId { get; set; }
        public Guid EventId { get; set; }
        public Guid PartnerId { get; set; }
        public string IsPaymentCompleted { get; set; }

        public string CompanyName { get; set; }
    }
}

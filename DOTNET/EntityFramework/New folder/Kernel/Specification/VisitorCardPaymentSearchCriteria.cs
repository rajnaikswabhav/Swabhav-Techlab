using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class VisitorCardPaymentSearchCriteria
    {
        public Guid EventId { get; set; }
        public Guid LoginId { get; set; }
        public string MobileNo { get; set; }
        public string ExhibitorName { get; set; }
        public DateTime CardPaymentDate { get; set; }
        public Guid ExhibitorId { get; set; }
    }
}

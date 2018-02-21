using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class EventLeadExhibitorMapSearchCriteria
    {
        public Guid SalesPersonId { get; set; }
        public Guid ExhibitorId { get; set; }
        public Guid EventId { get; set; }
    }
}

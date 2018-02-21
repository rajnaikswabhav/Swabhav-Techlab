using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class SalesTargetSearchCriteria
    {
        public Guid EventId { get; set; }
        public Guid ExhibitorIndustryTypeId { get; set; }
        public Guid SalesPersonId { get; set; }
        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }
    }
}

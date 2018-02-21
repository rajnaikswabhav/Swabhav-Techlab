using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
   public class StallSearchCriteria
    {
        public Guid sectionId { get; set; }
        public string IsBooked { get; set; }
        public int? StallSize { get; set; }
        public Guid EventId { get; set; }
    }
}

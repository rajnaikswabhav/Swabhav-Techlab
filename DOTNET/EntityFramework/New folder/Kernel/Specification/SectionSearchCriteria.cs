using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class SectionSearchCriteria
    {
        public Guid LayoutId { get; set; }
        public Guid SectionId { get; set; }
        public string Type { get; set; }
    }
}

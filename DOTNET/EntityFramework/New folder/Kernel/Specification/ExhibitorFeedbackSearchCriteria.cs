using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class ExhibitorFeedbackSearchCriteria
    {
        public Guid ExhibitorId { get; set; }
        public Guid EventId { get; set; }
        public int Objective { get; set; }
        public int Satisfaction { get; set; }
        public int QualityofVisitor { get; set; }
        public bool? TargetAudience { get; set; }
        public bool? ExpectedBusiness { get; set; }
        public int IIMTFSatisfaction { get; set; }
        public int IIMTFTeam { get; set; }
        public int IIMTFFacility { get; set; }
        public Guid MarketIntrested { get; set; }
    }
}

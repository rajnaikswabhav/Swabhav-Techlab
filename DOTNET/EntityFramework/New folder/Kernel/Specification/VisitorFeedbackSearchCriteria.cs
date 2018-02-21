using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class VisitorFeedbackSearchCriteria
    {
        public Guid VisitorId { get; set; }
        public Guid EventId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CountryId { get; set; }
        public Guid ExhibitorTypeId { get; set; }
        public int SpendRange { get; set; }
        public int ReasonForVisiting { get; set; }
        public int KnowAboutUs { get; set; }
        public int EventRating { get; set; }
        public bool? Recommendation { get; set; }
    }
}

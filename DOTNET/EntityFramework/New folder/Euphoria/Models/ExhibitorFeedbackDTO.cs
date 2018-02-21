using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitorFeedbackDTO
    {
        public string CompanyName { get; set; }
        public string ExhibitorName { get; set; }
        public int Satisfaction { get; set; }
        public int Objective { get; set; }
        public bool TargetAudience { get; set; }
        public int QualityOfVisitor { get; set; }
        public bool ExpectedBusiness { get; set; }
        public int IIMTFSatisfaction { get; set; }
        public int IIMTFTeam { get; set; }
        public int IIMTFFacility { get; set; }
        public virtual ICollection<VenueDTO> MarketIntrested { get; set; }
        public string Comment { get; set; }

        public ExhibitorFeedbackDTO()
        {
            MarketIntrested = new List<VenueDTO>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class SalesTargetListingDTO
    {
        public Guid Id { get; set; }
        public string SalesPerson { get; set; }
        public string ExhibitorIndustryType { get; set; }
        public string ExhibitorType { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Target { get; set; }
        public int TargetAchived { get; set; }
    }
}
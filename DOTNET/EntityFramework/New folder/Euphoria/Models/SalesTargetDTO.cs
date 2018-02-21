using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class SalesTargetDTO
    {
        public int Target { get; set; }
        public int TargetAchieved { get; set; }
        public Guid SalesPersonId { get; set; }
        public Guid ExhibitorIndustryTypeId { get; set; }
        public Guid ExhibitorTypeId { get; set; }
        public Guid StateId { get; set; }
        public Guid CountryId { get; set; }
    }
}
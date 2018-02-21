using System;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class ExhibitorSearchCriteria
    {
        public string CompanyName { get; set; }
        public string Pavilion { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Category { get; set; }
        public int StartRange { get; set; }
        public int EndRange { get; set; }
        public string ExhibitorRegistrationType { get; set; }
        public Guid CountryId { get; set; }
        public Guid ExhibitorTypeId { get; set; }
        public Guid ExhibitorIndustryTypeId { get; set; }
        public Guid ExhibitorRegistrationTypeId { get; set; }
    }
}
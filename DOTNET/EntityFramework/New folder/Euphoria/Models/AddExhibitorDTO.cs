using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class AddExhibitorDTO
    {
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public Guid ExhibitorIndustryTypeId { get; set; }
        public string Comment { get; set; }
    }
}
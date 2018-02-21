using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class StallAllocationDTO
    {
        public Guid PartnerId { get; set; }
        public Guid ExhibitorIndustryTypeId { get; set; }
        public Guid CountryId { get; set; }
        public Guid StateId { get; set; }

        public List<StallIdListDTO> StallIdListDTO { get; set; }

        public StallAllocationDTO()
        {
            StallIdListDTO = new List<StallIdListDTO>();
        }

    }
}
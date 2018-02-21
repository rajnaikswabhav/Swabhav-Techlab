using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitorStallAllocationDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid State { get; set; }

        public Guid Contry { get; set; }
        public int Pincode { get; set; }
        public List<CategoryDTO> Category { get; set; }
        public StallIdListDTO StallIdListDTO { get; set; }

    }
}
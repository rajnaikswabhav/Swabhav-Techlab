using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class PavilionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public ExhibitionDTO Exhibition { get; set; }
        //public IList<StallDTO> Stalls { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class StallDTO
    {
        public int StallNo { get; set; }
        public PavilionDTO Pavilion { get; set; }
    }
}
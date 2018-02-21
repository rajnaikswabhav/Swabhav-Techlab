using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class StallBarrierListDTO
    {
        public List<StallListDTO> StallListDTO { get; set; }
        public List<BarrierDTO> BarrierDTO { get; set; }

        public StallBarrierListDTO()
        {
            StallListDTO = new List<StallListDTO>();
            BarrierDTO = new List<BarrierDTO>();
        }
    }
}
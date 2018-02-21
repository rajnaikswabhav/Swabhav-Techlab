using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class StallIdListDTO
    {
        public Guid StallId { get; set; }
        public Guid HangerId { get; set; }
        public bool IsBooked { get; set; } 
    }
}
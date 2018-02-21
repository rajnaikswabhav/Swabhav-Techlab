using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class EventExhibitionDTO
    {
        public Guid ExhibitionId { get; set; }
        public string ExhibitionName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
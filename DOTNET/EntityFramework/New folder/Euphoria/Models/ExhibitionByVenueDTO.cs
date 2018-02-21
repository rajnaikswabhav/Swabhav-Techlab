using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitionByVenueDTO
    {
        public string Date { get; set; }
        public string Address { get; set; }
        public Guid EventId { get; set; }
        public List<ExhibitionDTO> Exhibition { get; set; }
    }

    //public class Exhibition
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public string StartDate { get; set; }
    //    public string EndDate { get; set; }
    //    public bool isActive { get; set; }
    //    public string Logo { get; set; }
    //    public string BgImage { get; set; }
    //}
}
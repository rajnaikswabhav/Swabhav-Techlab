using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitionDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
        [Required]
        public bool isActive { get; set; }
        public string Logo { get; set; }
        public string BgImage { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Guid VenueId { get; set; }
    }
}
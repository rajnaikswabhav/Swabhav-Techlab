using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitionHomeDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string StartDate { get; set; }
        [Required]
        public string EndDate { get; set; }
        [Required]
        public bool isActive { get; set; }
        public string BannerImage { get; set; }
        public string Bannertext { get; set; }
        public string Logo { get; set; }
        public string BgImage { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Guid VenueId { get; set; }
        public string VenueName { get; set; }
        public Guid EventId { get; set; }
    }
}
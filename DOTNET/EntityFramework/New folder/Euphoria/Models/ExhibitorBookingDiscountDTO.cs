using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitorBookingDiscountDTO
    {
        public Guid BookingExhibitorDiscountId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string EndDate { get; set; }
        public string Name { get; set; }
        public string BannerImage { get; set; }
        public List<StallDTO> Stalls { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public CountryDTO Country { get; set; }
        public StateDTO State { get; set; }

        public ExhibitorBookingDiscountDTO()
        {
            Stalls = new List<StallDTO>();
            Categories = new List<CategoryDTO>();
        }
    }
}
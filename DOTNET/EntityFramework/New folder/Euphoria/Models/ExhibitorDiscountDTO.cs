using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitorDiscountDTO
    {
        public Guid BookingExhibitorDiscountId { get; set; }
        public string Heading { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Country { get; set; }
        public string ProductUrl { get; set; }
        public string BannerUrl { get; set; }
        public int TotalClicks { get; set; }
        public int TotalGrabOffer { get; set; }
    }
}
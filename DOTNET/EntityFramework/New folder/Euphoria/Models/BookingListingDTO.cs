using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class BookingListingDTO
    {
        public Guid BookingId { get; set; }
        public string BookingDate { get; set; }
        public string BookingReferenceId { get; set; }
        public string CompanyName { get; set; }
        public int StallCount { get; set; }
        public double TotalAmount { get; set; }
        public double ReceivedAmount { get; set; }
        public double PendingAmount { get; set; }
        public string Comment { get; set; }

        public List<BookedStallListDTO> StallList { get; set; }

        public BookingListingDTO()
        {
            StallList = new List<BookedStallListDTO>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class BookingDTO
    {
        public Guid Id { get; set; }
        public string BookingRequestId { get; set; }
        public string DateOfBooking { get; set; }
        public string CompanyName { get; set; }
        public string HangerName { get; set; }
        public Guid HangerId { get; set; }
        public double FinalAmount { get; set; }
        public double AmountPaid { get; set; }
        public double Balance { get; set; }
        public string SalesPerson { get; set; }
        public int HangerHeight { get; set; }
        public int HangerWidth { get; set; }
        public string Comment { get; set; }
        public int Quantity { get; set; }

        public List<BookedStallListDTO> StallList { get; set; }
        public List<string> CategoryName { get; set; }

        public BookingDTO()
        {
            StallList = new List<BookedStallListDTO>();
            CategoryName = new List<string>();
        }
    }
}
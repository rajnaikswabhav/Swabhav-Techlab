using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class BookingRequestPostDTO
    {
        public string Action { get; set; }
        public bool Confirmed { get; set; }
        public string Status { get; set; }
        public Guid ExhibitorId { get; set; }
        public DateTime BookingDate { get; set; }
        public double TotalAmount { get; set; }
        public double FinalAmountTax { get; set; }
        public int DiscountPercent { get; set; }
        public int? DiscountAmount { get; internal set; }
        public string Comment { get; set; }

        public List<StallsListDTO> Liststall { get; set; }        
        public List<BookingInstallmentDTO> BookingInstallmentDTO { get; set; }
        public BookingRequestPostDTO()
        {
            Liststall = new List<StallsListDTO>();
            BookingInstallmentDTO = new List<BookingInstallmentDTO>();
        }
    }
    public class StallsListDTO
    {
        public Guid ExhibitionId { get; set; }
        public Guid EventId { get; set; }
        public Guid StallId { get; set; }
    }
}
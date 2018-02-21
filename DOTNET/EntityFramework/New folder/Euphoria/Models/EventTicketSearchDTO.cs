using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class EventTicketSearchDTO
    {
        public Guid Id { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string TokenNumber { get; set; }
        public string TicketDate { get; set; }
        public string ExhibitionTime { get; set; }
        public int NumberOfTicket { get; set; }
        public double TotalPriceOfTicket { get; set; }
        public Guid DiscountCouponId { get; set; }
        public string TicketTypeId { get; set; }
        public string TicketName { get; set; }
        public string VenueName { get; set; }
        public bool IsPayOnLocation { get; set; }
        public string BgColor { get; set; }
        public string EventAddress { get; set; }
        public int? Device { get; set; }
        public string PaymentType { get; set; }
        public string Status { get; set; }
    }
}
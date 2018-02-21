using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class LuckyDrawVisitorDTO
    {
        public Guid ticketId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string FacebookId { get; set; }
        public string TicketDate { get; set; }
        public Guid EventTicketTypeId { get; set; }
        public int NumberOfTicket { get; set; }
        public double TotalPriceOfTicket { get; set; }
        public bool IsPayOnLocation { get; set; }
        public bool Gender { get; set; }
        public string PhysicalTicketSrNo { get; set; }
        public Guid StaffId { get; set; }
        public string StaffName { get; set; }
    }
}
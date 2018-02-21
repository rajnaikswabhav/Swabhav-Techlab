using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class EventTicketDashboardDTO
    {
        public int TotalNumberOfVisitor { get; set; }
        public int TotalCountOfTickets { get; set; }
        public double TotalTicketPrice { get; set; }
        public int TotalWebTickets { get; set; }
        public double TotalWebTicketPrice { get; set; }
        public int? TotalMobileTickets { get; set; }
        public double TotalMobileTicketPrice { get; set; }
        public int TotalPayAtLocationTickets { get; set; }
        public double TotalPayAtLocationPrice { get; set; }
        public int TotalOnlineTickets { get; set; }
        public double TotalOnlineTicketPrice { get; set; }
        public int TotalIphoneTickets { get; set; }
        public double TotalIphoneTicketsPrice { get; set; }
        public int TotalAndroidTickets { get; set; }
        public double TotalAndroidTicketsPrice { get; set; }
    }
}
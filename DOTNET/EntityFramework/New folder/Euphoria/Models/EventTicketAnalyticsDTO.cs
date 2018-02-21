using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class EventTicketAnalyticsDTO
    {
        public string Date { get; set; }
        public int MobileTickets { get; set; }
        public int WebTickets { get; set; }
        public int OnlineTicktes { get; set; }
        public int PayAtLocationTickets { get; set; }
        public int TotalTickets { get; set; }
        public int TotalVisitors { get; set; }
    }
}
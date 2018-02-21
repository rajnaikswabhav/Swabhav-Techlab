using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class DashboardDTO
    {
        public int TotalPartners { get; set; }
        public int TotalPayments { get; set; }
        public int TotalPaymentsApprovals { get; set; }
        public int TotalApprovedPayments { get; set; }
        public int TotalExhibitor { get; set; }
        public int TotalLeads { get; set; }
        public int TotalHangers { get; set; }
        public int TotalStallBooking { get; set; }
        public int TotalBookingRequest { get; set; }
        public int TotalCancellation { get; set; }
        public int TotalVisitors { get; set; }
        public int TotalVisitorComplaints { get; set; }
        public int TotalVisitorFeedback { get; set; }
        public int TotalTickets { get; set; }
        public int TotalExhibitorComplants { get; set; }
        public int TotalExhibitorFeedback { get; set; }
        public int TotalExhibitorEnquiry { get; set; }
        public int TotalCardPayments { get; set; }
    }
}
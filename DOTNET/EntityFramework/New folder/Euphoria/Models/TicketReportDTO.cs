using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class TicketReportDTO
    {
        public bool isMobileVerified { get; set; }
        public string dateOfBooking { get; set; }
        public string bookingId { get; set; }
        public string emailId { get; set; }
        public string phoneNo { get; set; }
        public string numberOfTicket { get; set; }
        public string ticketDate { get; set; }
        public string ticketAmount { get; set; }
        public string paymentMode { get; set; }
        public bool isPaymentCompleted { get; set; }
        public string pincode { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class VisitorPaymentDetailsDTO
    {
        public Guid Id { get; set; }
        public string VisitorName { get; set; }
        public string PurchaseDate { get; set; }
        public double Amount { get; set; }
        public int PaymentModeValue { get; set; }
        public string PaymentMode { get; set; }
        public int PointsEarned { get; set; }
        public Guid PavilionId { get; set; }
        public string PavilionName { get; set; }
        public Guid ExhibitorId { get; set; }
        public string ExhibitorName { get; set; }
        public string EventName { get; set; }
        public string VisitorMobileNo { get; set; }
        public string FilePath { get; set; }
        public string VenueName { get; set; }
    }
}
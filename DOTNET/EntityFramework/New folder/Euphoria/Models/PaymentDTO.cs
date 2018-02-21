using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class PaymentDTO
    {
        public Guid Id { get; set; }
        public string InvoiceNo { get; set; }
        public double AmountPaid { get; set; }
        public bool IsPayOnLocation { get; set; }
        public string PaymentDate { get; set; }
        public int Quantity { get; set; }
        public double TotalAmount { get; set; }
        public string ExhibitorName { get; set; }
        public string CompanyName { get; set; }
        public double AmountRemaining { get; set; }
        public string Address { get; set; }
        public double AlreadyPaid { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class VisitorCardPaymentDTO
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string ExhibitorName { get; set; }
        public string TransactionId { get; set; }
        public string Invoice { get; set; }
        public int Amount { get; set; }
        public string TransactionDate { get; set; }
    }
}
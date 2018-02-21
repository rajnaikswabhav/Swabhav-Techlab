using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class VisitorCardPaymentByDateDTO
    {
        public string PaymentDate { get; set; }
        public int TotalCount { get; set; }
        public int TotalAmount { get; set; }
    }
}
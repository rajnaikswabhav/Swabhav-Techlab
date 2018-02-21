using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class BookingStatusDTO
    {
        public string Item { get; set; }
        public int TotalStalls { get; set; }
        public int TotalAmount { get; set; }
        public int AmountPaid { get; set; }
        public int TotalArea { get; set; }
    }
}
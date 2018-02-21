using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class BookingAmountCountDTO
    {
        public int TotalAmount { get; set; }
        public int ReceivedAmount { get; set; }
        public int PendingAmount { get; set; }
    }
}
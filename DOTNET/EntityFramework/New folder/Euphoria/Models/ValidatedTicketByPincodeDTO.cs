using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ValidatedTicketByPincodeDTO
    {
        public int Pincode { get; set; }
        public int TotalVisitorCount { get; set; }
        public int TotalTicketCount { get; set; }
    }
}
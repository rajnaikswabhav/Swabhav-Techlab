﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class AccountDashboardDTO
    {
        public int TotalPaymentsApproval { get; set; }
        public int TotalApprovedPayments { get; set; }
        public int TotalBookings { get; set; }
    }
}
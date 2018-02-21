using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class VisitorCardPaymentAnalysisDTO
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public int TotalAmount { get; set; }
        public List<ExhibitorsDTO> ExhibitorsDTO { get; set; }

        public VisitorCardPaymentAnalysisDTO()
        {
            ExhibitorsDTO = new List<ExhibitorsDTO>();
        }

    }
    public class ExhibitorsDTO
    {
        public string ExhibitorName { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }
    }
}
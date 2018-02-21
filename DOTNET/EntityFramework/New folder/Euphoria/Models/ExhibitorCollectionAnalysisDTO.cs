using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitorCollectionAnalysisDTO
    {
        public string ExhibitorName { get; set; }
        public string CompanyName { get; set; }
        public int TotalAmount { get; set; }
        public List<VisitorsDTO> VisitorsDTO { get; set; }

        public ExhibitorCollectionAnalysisDTO()
        {
            VisitorsDTO = new List<VisitorsDTO>();
        }
    }
    public class VisitorsDTO
    {
        public string VisitorName { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }
    }
}
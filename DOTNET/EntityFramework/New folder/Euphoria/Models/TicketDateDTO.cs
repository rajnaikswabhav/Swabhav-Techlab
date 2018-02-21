using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class PriceDetailDTO
    {
        public int? BusinessHrs { get; set; }
        public int NonBusinessHrs { get; set; }
        public int? BusinessHrsDiscount { get; set; }
        public int NonBusinessHrsDiscount { get; set; }
    }
    public class TicketDateDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public PriceDetailDTO PriceDetails { get; set; }
        public List<string> AllAvailableListOfDate { get; set; }
        public int DisplayOrder { get; set; }
       // public List<string> AllWeeekendListOfDate { get; set; }       
    }
}
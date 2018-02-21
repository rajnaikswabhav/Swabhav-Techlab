using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class DisplayExhitonTicketDTO
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string BgImage { get; set; }
    }

    public class TicketHistoryDTO
    {        
        public Guid Id { get; set; }
        public string TokenNumber { get; set; }
        public string TicketDate { get; set; }
        public string ExhibitionTime { get; set; }
        public int NumberOfTicket { get; set; }
        public double TotalPriceOfTicket { get; set; }
        public string TicketTypeId { get; set; }
        public string TicketName { get; set; }
        public string VenueName { get; set; }
        public bool IsPayOnLocation { get; set; }
        public string BgColor { get; set; }
        public string EventAddress { get; set; }

        public List<DisplayExhitonTicketDTO> DisplayExhibitionTicket { get; set; }

        public TicketHistoryDTO()
        {
            DisplayExhibitionTicket = new List<DisplayExhitonTicketDTO>();
        }
    }
}
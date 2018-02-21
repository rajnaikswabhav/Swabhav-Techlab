using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class EventDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string BookingStartDate { get; set; }
        public bool isActive { get; set; }
        public string EventTime { get; set; }
        public string VenueId { get; set; }
        public string UpdatedById { get; set; }
        public VenueDTO VenueDTO { get; set; }
        public List<EventExhibitionDTO> EventExhibitions { get; set; }
        public List<EventTicketTypeDTO> EventTicketType { get; set; }
        public EventDTO()
        {
            EventExhibitions = new List<EventExhibitionDTO>();
            EventTicketType = new List<EventTicketTypeDTO>();
        }
    }
}
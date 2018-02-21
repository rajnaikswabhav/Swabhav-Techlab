using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class PriceDetailsDTO
    {
        public int businessHrs { get; set; }
        public int nonBusinessHrs { get; set; }
        public int businessHrsDiscount { get; set; }
        public int nonBusinessHrsDiscount { get; set; }
    }

    public class TicketTypeDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Type { get; set; }
       
        public PriceDetailsDTO PriceDetails { get; set; }
        public string Description { get; set; }
    }

    public enum TypeOfTicket {         
        AllDays=10,Single=1,Weekend=2
    }
}
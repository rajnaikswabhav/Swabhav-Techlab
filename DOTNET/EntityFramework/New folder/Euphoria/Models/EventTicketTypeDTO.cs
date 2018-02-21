using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class EventTicketTypeDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Type { get; set; }
        public int? businessHrs { get; set; }
        public int nonBusinessHrs { get; set; }
        public int? businessHrsDiscount { get; set; }
        public int nonBusinessHrsDiscount { get; set; }
        public string Description { get; set; }
    }
}
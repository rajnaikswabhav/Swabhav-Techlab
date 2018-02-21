using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class VenueDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string City { get; set; }

        //[Required]
        //public string State { get; set; }
        //[Required]
        //public string Address { get; set; }    
    }
}
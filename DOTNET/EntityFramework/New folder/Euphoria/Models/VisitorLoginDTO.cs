using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class VisitorLoginDTO
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string MobileNo { get; set; }
    }
}
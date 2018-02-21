using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class AddPartnerDTO
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Color { get; set; }
        public string PhoneNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
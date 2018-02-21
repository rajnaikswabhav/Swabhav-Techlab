using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class SendVisitorEmailDTO
    {
        public int FromPincode { get; set; }
        public int ToPincode { get; set; }
        public bool Gender { get; set; }
        public List<Guid> CategoryId { get; set; }

        public SendVisitorEmailDTO()
        {
            CategoryId = new List<Guid>();
        }
    }
}
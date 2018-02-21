using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class BookingSalesPersoneDTO
    {
        public List<Guid> BookingIdList { get; set; }
        public Guid SalesPersonId { get; set; }

        public BookingSalesPersoneDTO()
        {
            BookingIdList = new List<Guid>();
        }
    }
}
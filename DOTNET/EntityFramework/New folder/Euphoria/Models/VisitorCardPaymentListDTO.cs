using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class VisitorCardPaymentListDTO
    {
        public Guid LoginId { get; set; }
        public string LoignUser { get; set; }
        public int totalEntry { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class BillGeneratorDTO
    {
        public string merchantTxnId { get; set; }       
        public string requestSignature { get; set; }
        public string merchantAccessKey { get; set; }
        public string returnUrl { get; set; }

        public CitrussAmount amount { get; set; }
    }
    public class CitrussAmount
    {
        public string value { get; set; }
        public string currency { get; set; }
    }
}

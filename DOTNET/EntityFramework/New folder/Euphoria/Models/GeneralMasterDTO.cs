using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class GeneralMasterDTO
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public int Key { get; set; }
        public bool Active { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitorReportDTO
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public int TotalCount { get; set; }
    }
}
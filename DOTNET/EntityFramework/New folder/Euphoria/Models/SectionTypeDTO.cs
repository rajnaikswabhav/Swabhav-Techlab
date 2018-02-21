using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class SectionTypeDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string sectionType { get; set; }
    }
}
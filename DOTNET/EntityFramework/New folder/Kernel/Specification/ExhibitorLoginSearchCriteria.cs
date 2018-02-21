using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class ExhibitorLoginSearchCriteria
    {
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        public string ExhibitorName { get; set; }
    }
}

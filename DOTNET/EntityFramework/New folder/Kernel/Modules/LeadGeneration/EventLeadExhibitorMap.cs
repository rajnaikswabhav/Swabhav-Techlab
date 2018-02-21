using Modules.EventManagement;
using Modules.SecurityManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.Modules.LeadGeneration
{
    [Table("EVENTLEADEXHIBITORMAP")]
    public class EventLeadExhibitorMap : MasterEntity
    {
        public virtual Event Event { get; set; }
        [Required]
        public virtual Exhibitor Exhibitor { get; set; }
        public virtual Login Login { get; set; }
    }
}

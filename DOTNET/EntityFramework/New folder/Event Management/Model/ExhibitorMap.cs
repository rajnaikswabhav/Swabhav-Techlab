using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;

namespace Event_Management.Model
{
    [Table("EXHIBITORMAP")]
    public class ExhibitorMap : MasterEntity
    {
        public virtual EventExhibitionMap EventExhibitionMap { get; set; }
        public virtual Exhibitor Exhibitor { get; set; }
    }
}

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
    [Table("EVENTEXHIBITIONMAP")]
    public class EventExhibitionMap : MasterEntity
    {
        public virtual Exhibition Exhibition { get; set; }
        public virtual Event Event { get; set; }
    }
}

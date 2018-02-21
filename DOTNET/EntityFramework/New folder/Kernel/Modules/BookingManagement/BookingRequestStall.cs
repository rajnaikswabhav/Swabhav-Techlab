using Modules.EventManagement;
using Modules.LayoutManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.Modules.BookingManagement
{
    [Table("BOOKINGREQUESTSTALL")]
    public class BookingRequestStall : MasterEntity
    {
        public virtual BookingRequest BookingRequest { get; set; }
        public virtual Event Event { get; set; }
        public virtual Exhibition Exhibition { get; set; }

        public virtual Stall Stall { get; set; }
    }
}

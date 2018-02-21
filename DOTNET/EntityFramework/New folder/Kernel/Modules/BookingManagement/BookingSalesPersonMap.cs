using Modules.SecurityManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Modules.BookingManagement
{
    [Table("BOOKINGSALESPERSONMAP")]
    public class BookingSalesPersonMap : MasterEntity
    {
        public virtual Booking Booking { get; set; }
        public virtual Login Login { get; set; }
    }
}

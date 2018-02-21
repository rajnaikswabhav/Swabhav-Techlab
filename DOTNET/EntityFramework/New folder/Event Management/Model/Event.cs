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
    [Table("EVENT")]
    public class Event : MasterEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime BookingStartDate { get; set; }
        public bool isActive { get; set; }

        public virtual Venue Venue { get; set; }
    }
}

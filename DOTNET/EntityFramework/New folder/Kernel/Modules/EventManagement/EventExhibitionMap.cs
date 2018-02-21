using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;

namespace Modules.EventManagement
{
    [Table("EVENTEXHIBITIONMAP")]
    public class EventExhibitionMap : MasterEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Exhibition Exhibition { get; set; }
        public virtual Event Event { get; set; }

        public EventExhibitionMap()
        {
        }

        public EventExhibitionMap(DateTime startDate, DateTime endDate) : this()
        {
            Validate(startDate,endDate);

            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        private static void Validate(DateTime startDate, DateTime endDate)
        {

        }

        public static EventExhibitionMap Create(DateTime startDate, DateTime endDate)
        {
            Validate(startDate, endDate);
            return new EventExhibitionMap(startDate, endDate);
        }

        public void Update(DateTime startDate, DateTime endDate)
        {
            Validate(startDate, endDate);

            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}

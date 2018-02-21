using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class LuckyDrawEventTicketReportSearchCriteria
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid AdminId { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public DateTime DateForSearch { get; set; } 
    }
}

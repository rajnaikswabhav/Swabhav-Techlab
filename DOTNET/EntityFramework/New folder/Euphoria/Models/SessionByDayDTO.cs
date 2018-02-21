using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class SessionByDayDTO
    {
        public string StaffName { get; set; }
        public List<DayCountDTO> DayCountDTO { get; set; }
        public SessionByDayDTO()
        {
            DayCountDTO = new List<DayCountDTO>();
        }
    }
    public class DayCountDTO
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }
}
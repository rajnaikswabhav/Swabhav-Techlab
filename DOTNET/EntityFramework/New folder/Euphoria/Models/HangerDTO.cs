using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class HangerDTO
    {
        public Guid HangerId { get; set; }
        public Guid exhibitionId { get; set; }
        public string HangerName { get; set; }
        public int TotalStalls { get; set; }
        public int StallsBooked { get; set; }
        public int RemainingStalls { get; set; }
        public int Hight { get; set; }
        public int Width { get; set; }
    }
}
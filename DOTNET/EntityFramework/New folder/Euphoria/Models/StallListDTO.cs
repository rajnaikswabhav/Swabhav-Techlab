using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class StallListDTO
    {
        public Guid Id { get; set; }
        public int StallNo { get; set; }
        public bool IsBooked { get; set; }
        public bool IsRequested { get; set; }
        public double Price { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X_Coordinate { get; set; }
        public int Y_Coordinate { get; set; }
        public int? StallSize { get; set; }
        public Guid PartnerId { get; set; }
        public string PartnerColor { get; set; }

        public StallListDTO()
        {
        }
    }
}
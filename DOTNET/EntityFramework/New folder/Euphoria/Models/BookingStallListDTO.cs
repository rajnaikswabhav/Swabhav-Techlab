using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class BookingStallListDTO
    {
        public Guid Id { get; set; }
        public int StallNo { get; set; }
        public bool IsBooked { get; set; }
        public double Price { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X_Coordinate { get; set; }
        public int Y_Coordinate { get; set; }
        public bool IsRequested { get; set; }
        public Guid PartnerId { get; set; }
        public string PartnerColor { get; set; }

        public string CompanyName { get; set; }
        public string Location { get; set; }
        public List<string> Product { get; set; }
        public int? SizeOfStall { get; set; }
        public double TotalAmount { get; set; }
        public double RecivedAmount { get; set; }
        public double Balance { get; set; }
        public BookingStallListDTO()
        {
            Product = new List<string>();
        }
    }
}
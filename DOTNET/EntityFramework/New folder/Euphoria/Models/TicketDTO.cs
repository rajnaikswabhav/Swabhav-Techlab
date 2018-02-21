using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class DisplayExhibitionDTO
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public string BgImage { get; set; }
    }
    public class TicketDTO
    {
        public Guid Id { get; set; }
        public string TokenNumber { get; set; }
        public string TicketDate { get; set; }
        public string ExhibitionTime { get; set; }
        public int NumberOfTicket { get; set; }
        public double TotalPriceOfTicket { get; set; }
        public Guid DiscountCouponId { get; set; }
        public string TicketTypeId { get; set; }
        public string TicketName { get; set; }
        public string VenueName { get; set; }
        public bool IsPayOnLocation { get; set; }
        public string BgColor { get; set; }
        public string EventAddress { get; set; }
        public int? Device { get; set; }
        public string PaymentType { get; set; }

        public List<DisplayExhibitionDTO> DisplayExhibitions { get; set; }

        public TicketDTO()
        {
            DisplayExhibitions = new List<DisplayExhibitionDTO>();
        }
    }
}

public class TicketColors
{
    private string[] colors = {
    "#FFD700", "#2ECC71", "#FF69B4", "#FFD700", "#FF0000", "#00FF00", "#DEB887", "#DC143C", "#008B8B", "#BB8FCE", "#00FFD2"
    };

    public string this[int index]
    {
        get
        {
            if (colors.Length > index)
                return colors[index];

            return "#FFFFFF";
        }
    }
}


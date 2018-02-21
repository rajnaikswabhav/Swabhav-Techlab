using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class BookingRequestDTO
    {
        public Guid Id { get; set; }
        public string BookingRequestId { get; set; }
        public string CompanyName { get; set; }
        public string EmailId { get; set; }
        public string HangerName { get; set; }
        public Guid HangerId { get; set; }
        public int HangerWidth { get; set; }
        public int HangerHeight { get; set; }
        public string DateOfBooking { get; set; }
        public int Quantity { get; set; }
        public double TotalAmount { get; set; }
        public double? FinalAmount { get; set; }
        public double AmountPaid { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public double RemainingAmount { get; set; }
        public string SalesPerson { get; set; }
        public int? DiscountInPercent { get; set; }
        public int? DiscountInAmount { get; set; }

        public List<BookedStallListDTO> stallList { get; set; }
        public List<string> CategoryName { get; set; }
        public List<BookingInstallmentDTO> BookingInstallmentDTO { get; set; }
        public BookingRequestDTO()
        {
            stallList = new List<BookedStallListDTO>();
            CategoryName = new List<string>();
            BookingInstallmentDTO = new List<BookingInstallmentDTO>();
        }
    }

    public class BookedStallListDTO
    {
        public Guid StallId { get; set; }
        public double Price { get; set; }
        public int StallNo { get; set; }
        public int? StallSize { get; set; }
    }
}
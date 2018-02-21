using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitorAllocationStallDTO
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public int PinCode { get; set; }
        public Guid Country { get; set; }
        public Guid State { get; set; }
        public double TotalAmount { get; set; }
        public Guid EventId { get; set; }
        public Guid ExhibitorTypeId { get; set; }
        public Guid ExhibitorIndustryTypeId { get; set; }
        public Guid ExhibitorStatusId { get; set; }

        public List<Guid> CategoryId { get; set; }
        //public List<BookingStallsListDTO> BookingStallsListDTO { get; set; }

        public ExhibitorAllocationStallDTO()
        {
            //BookingStallsListDTO = new List<BookingStallsListDTO>();
            CategoryId = new List<Guid>();
        }
    }
    public class BookingRequestStallDTO
    {
        public double TotalAmount { get; set; }
        public double FinalAmount { get; set; }
        public string Comment { get; set; }
        public List<BookingStallsListDTO> BookingStallsListDTO { get; set; }
        public List<BookingInstallmentDTO> BookingInstallmentDTO { get; set; }
        public BookingRequestStallDTO()
        {
            BookingStallsListDTO = new List<BookingStallsListDTO>();
            BookingInstallmentDTO = new List<BookingInstallmentDTO>();
        }
    }
    public class BookingInstallmentDTO
    {
        public Guid BookingInstallmentId { get; set; }
        public int Percent { get; set; }
        public int Amount { get; set; }
        public bool IsPaid { get; set; }
        public int Order { get; set; }
        public DateTime PaymentDate { get; set; }
    }

    public class BookingStallsListDTO
    {
        public Guid StallId { get; set; }
        public Guid HangerId { get; set; }
        public Guid ExhibitionId { get; set; }
        public double StallPrice { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class BookingPaymentDTO
    {
        public Guid Id { get; set; }
        public double AmountPaid { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentID { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string UTRNo { get; set; }
        public int PaymentStatus { get; set; }
        public string PaymentStatusName { get; set; }
        public bool IsPaymentApprove { get; set; }
        public string ExhibitorName { get; set; }
        public string CompanyName { get; set; }
        public string EventName { get; set; }
        public string SalesPerson { get; set; }
        public Guid BookingInstalmentId { get; set; }
        public double TotalAmount { get; set; }
        public double TotalAmountPaid { get; set; }
        public double RemainingAmount { get; set; }
        public int TotalStalls { get; set; }


        public List<BookingInstallmentDTO> BookingInstallmentDTO { get; set; }

        public BookingPaymentDTO()
        {
            BookingInstallmentDTO = new List<BookingInstallmentDTO>();
        }
    }
}
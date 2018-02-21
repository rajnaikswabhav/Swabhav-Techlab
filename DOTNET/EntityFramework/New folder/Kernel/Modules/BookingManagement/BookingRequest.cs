using Modules.EventManagement;
using Modules.LayoutManagement;
using Modules.SecurityManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.Modules.BookingManagement
{
    [Table("BOOKINGREQUEST")]
    public class BookingRequest : MasterEntity
    {
        public string BookingRequestId { get; set; }
        public string Action { get; set; }
        public bool Confirmed { get; set; }
        public string Status { get; set; }
        public DateTime BookingRequestDate { get; set; }
        public double TotalAmount { get; set; }
        public double FinalAmountTax { get; set; }
        public int? DiscountPercent { get; set; }
        public int? DiscountAmount { get; set; }
        public string Comment { get; set; }

        public virtual Section Section { get; set; }
        public virtual Login Login { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual BookingRequest PreviousBookingrequest { get; set; }
        public virtual Event Event { get; set; }
        public virtual Exhibitor Exhibitor { get; set; }

        public BookingRequest()
        {
        }

        public BookingRequest(string bookingRequestId, string action, bool confirmed, string status, DateTime bookingRequestDate, double totalAmount, double finalAmount, int? discountPercent, int? discountAmount, string comment) : this()
        {
            Validate(bookingRequestId, action, confirmed, status, bookingRequestDate);
            this.BookingRequestId = bookingRequestId;
            this.Action = action;
            this.Confirmed = confirmed;
            this.Status = status;
            this.BookingRequestDate = bookingRequestDate;
            this.TotalAmount = totalAmount;
            this.FinalAmountTax = finalAmount;
            this.DiscountPercent = discountPercent;
            this.DiscountAmount = discountAmount;
            this.Comment = comment;
        }

        private static void Validate(string bookingRequestId, string action, bool confirmed, string status, DateTime bookingRequestDate)
        {
            if (String.IsNullOrEmpty(action))
                throw new ValidationException("Invalid Action Name");
            if (String.IsNullOrEmpty(bookingRequestId))
                throw new ValidationException("Invalid BookingRequestId");
        }

        public static BookingRequest Create(string bookingRequestId, string action, bool confirmed, string status, DateTime bookingRequestDate, double totalAmount, double finalAmount, int? discountPercent, int? discountAmount, string comment)
        {
            Validate(bookingRequestId, action, confirmed, status, bookingRequestDate);
            return new BookingRequest(bookingRequestId, action, confirmed, status, bookingRequestDate, totalAmount, finalAmount, discountPercent, discountAmount, comment);
        }

        public void Update(string bookingRequestId, string action, bool confirmed, string status, DateTime bookingRequestDate, double totalAmount, double finalAmount, int? discountPercent, int? discountAmount, string comment)
        {
            Validate(bookingRequestId, action, confirmed, status, bookingRequestDate);
            this.BookingRequestId = bookingRequestId;
            this.Action = action;
            this.Confirmed = confirmed;
            this.Status = status;
            this.BookingRequestDate = bookingRequestDate;
            this.TotalAmount = totalAmount;
            this.FinalAmountTax = finalAmount;
            this.DiscountPercent = discountPercent;
            this.DiscountAmount = discountAmount;
            this.Comment = comment;
        }
    }
}

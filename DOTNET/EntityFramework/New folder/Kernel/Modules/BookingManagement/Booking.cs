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
    [Table("BOOKING")]
    public class Booking : MasterEntity
    {
        public string BookingId { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime BookingDate { get; set; }
        public bool BookingAccepted { get; set; }
        public bool IsPaymentCompleted { get; set; }
        public double TotalAmount { get; set; }
        public double FinalAmountTax { get; set; }
        public int? DiscountPercent { get; set; }
        public int? DiscountAmount { get; set; }
        public string Comment { get; set; }

        public virtual Section Section { get; set; }
        public virtual Login Login { get; set; }
        public virtual Exhibitor Exhibitor { get; set; }
        public virtual Event Event { get; set; }

        public Booking()
        {
        }

        public Booking(string bookingId, string action, string status, DateTime bookingDate, bool bookingAccepted, bool isPaymentCompleted, double totalAmount, double finalAmount, int? discountPercent, int? discountAmount, string comment) : this()
        {
            Validate(bookingId, action, status, bookingDate, bookingAccepted, isPaymentCompleted, discountPercent);
            this.BookingId = bookingId;
            this.Action = action;
            this.Status = status;
            this.BookingDate = bookingDate;
            this.BookingAccepted = bookingAccepted;
            this.IsPaymentCompleted = isPaymentCompleted;
            this.TotalAmount = totalAmount;
            this.FinalAmountTax = finalAmount;
            this.DiscountPercent = discountPercent;
            this.DiscountAmount = DiscountAmount;
            this.Comment = comment;
        }

        private static void Validate(string bookingId, string action, string status, DateTime bookingDate, bool bookingAccepted, bool isPaymentCompleted, int? discount)
        {
            if (String.IsNullOrEmpty(bookingId))
                throw new ValidationException("Invalid BookingId");
            if (string.IsNullOrEmpty(action))
                throw new ValidationException("Invalid Action");
            if (string.IsNullOrEmpty(status))
                throw new ValidationException("Invalid Status");
        }

        public static Booking Create(string bookingId, string action, string status, DateTime bookingDate, bool bookingAccepted, bool isPaymentCompleted, double totalAmount, double finalAmount, int? discountPercent, int? discountAmount, string comment)
        {
            Validate(bookingId, action, status, bookingDate, bookingAccepted, isPaymentCompleted, discountPercent);
            return new Booking(bookingId, action, status, bookingDate, bookingAccepted, isPaymentCompleted, totalAmount, finalAmount, discountPercent, discountAmount, comment);
        }

        public void Update(string bookingId, string action, string status, DateTime bookingDate, bool bookingAccepted, bool isPaymentCompleted, double totalAmount, double finalAmount, int? discountPercent, int? discountAmount, string comment)
        {
            Validate(bookingId, action, status, bookingDate, bookingAccepted, isPaymentCompleted, discountPercent);
            this.BookingId = bookingId;
            this.Action = action;
            this.Status = status;
            this.BookingDate = bookingDate;
            this.BookingAccepted = bookingAccepted;
            this.IsPaymentCompleted = isPaymentCompleted;
            this.TotalAmount = totalAmount;
            this.FinalAmountTax = finalAmount;
            this.DiscountPercent = discountPercent;
            this.DiscountAmount = discountAmount;
            this.Comment = comment;
        }
    }
}

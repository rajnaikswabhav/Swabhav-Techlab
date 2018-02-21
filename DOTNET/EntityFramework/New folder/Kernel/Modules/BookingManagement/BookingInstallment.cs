using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Modules.BookingManagement
{
    [Table("BOOKINGINSTALLNENT")]
    public class BookingInstallment : MasterEntity
    {
        public int Percent { get; set; }
        public int Amount { get; set; }
        public bool IsPaid { get; set; }
        public int Order { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual Booking Booking { get; set; }

        public BookingInstallment()
        {
        }

        public BookingInstallment(int percent, int amount, bool isPaid, int order, DateTime paymentDate) : this()
        {
            Validate(percent, amount, isPaid, order);

            this.Percent = percent;
            this.Amount = amount;
            this.IsPaid = isPaid;
            this.Order = order;
            this.PaymentDate = paymentDate;
        }

        private static void Validate(int percent, int amount, bool isPaid, int order)
        {
        }

        public static BookingInstallment Create(int percent, int amount, bool isPaid, int order, DateTime paymentDate)
        {
            return new BookingInstallment(percent, amount, isPaid, order, paymentDate);
        }

        public void Update(int percent, int amount, bool isPaid, int order, DateTime paymentDate)
        {
            Validate(percent, amount, isPaid, order);

            this.Percent = percent;
            this.Amount = amount;
            this.IsPaid = isPaid;
            this.Order = order;
            this.PaymentDate = paymentDate;
        }
    }
}
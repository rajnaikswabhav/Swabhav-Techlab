using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Modules.BookingManagement
{
    [Table("BOOKINGREQUESTINSTALLMENT")]
    public class BookingRequestInstallment : MasterEntity
    {
        public int Percent { get; set; }
        public int Amount { get; set; }
        public bool IsPaid { get; set; }
        public int Order { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual BookingRequest BookingRequest { get; set; }

        public BookingRequestInstallment()
        {

        }
        public BookingRequestInstallment(int percent, int amount, bool isPaid, int order, DateTime paymentDate) : this()
        {
            Validate(percent, amount, isPaid);

            this.Percent = percent;
            this.Amount = amount;
            this.IsPaid = isPaid;
            this.Order = order;
            this.PaymentDate = paymentDate;
        }

        private static void Validate(int percent, int amount, bool isPaid)
        {

        }

        public static BookingRequestInstallment Create(int percent, int amount, bool isPaid, int order, DateTime paymentDate)
        {
            return new BookingRequestInstallment(percent, amount, isPaid, order, paymentDate);
        }

        public void Update(int percent, int amount, bool isPaid, int order, DateTime paymentDate)
        {
            Validate(percent, amount, isPaid);

            this.Percent = percent;
            this.Amount = amount;
            this.IsPaid = isPaid;
            this.Order = order;
            this.PaymentDate = paymentDate;
        }
    }
}

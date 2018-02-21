using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;

namespace Techlabs.Euphoria.Kernel.Modules.PaymentManagement
{
    [Table("PAYMENT")]
    public class Payment : MasterEntity
    {
        public double AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string InvoiceNo { get; set; }
        public bool IsPayOnLocation { get; set; }
        public string TxnIdForPaymentGateWay { get; set; }
        public bool IsPaymentDone { get; set; }

        public virtual Booking Booking { get; set; }

        public Payment()
        { 
        }

        public Payment(double amountPaid, DateTime paymentDate,string invoiceNo,bool isPayOnLocation,string txnIdForPaymentGateWay) : this()
        {
            Validate(amountPaid, paymentDate, invoiceNo, isPayOnLocation, txnIdForPaymentGateWay);

            this.AmountPaid = amountPaid;
            this.PaymentDate = paymentDate;
            this.InvoiceNo = invoiceNo;
            this.IsPayOnLocation = isPayOnLocation;
            this.TxnIdForPaymentGateWay = txnIdForPaymentGateWay;
        }

        private static void Validate(double amountPaid, DateTime paymentDate, string invoiceNo, bool isPayOnLocation, string txnIdForPaymentGateWay)
        {
            if (String.IsNullOrEmpty(invoiceNo))
                throw new ValidationException("Invalid InvoiceNo");
        }

        public static Payment Create(double amountPaid, DateTime paymentDate, string invoiceNo, bool isPayOnLocation, string txnIdForPaymentGateWay)
        {
            return new Payment(amountPaid, paymentDate, invoiceNo,isPayOnLocation,txnIdForPaymentGateWay);
        }

        public void Update(double amountPaid, DateTime paymentDate, string invoiceNo, bool isPayOnLocation, string txnIdForPaymentGateWay)
        {
            Validate(amountPaid, paymentDate, invoiceNo, isPayOnLocation, txnIdForPaymentGateWay);

            this.AmountPaid = amountPaid;
            this.PaymentDate = paymentDate;
            this.InvoiceNo = invoiceNo;
            this.IsPayOnLocation = isPayOnLocation;
            this.TxnIdForPaymentGateWay = txnIdForPaymentGateWay;
        }
    }
}

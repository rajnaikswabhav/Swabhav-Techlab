using Modules.EventManagement;
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
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;

namespace Techlabs.Euphoria.Kernel.Modules.PaymentManagement
{
    [Table("PAYMENTDETAILS")]
    public class PaymentDetails : MasterEntity
    {
        public double AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentID { get; set; }
        public int PaymentMode { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string UTRNo { get; set; }
        public int PaymentStatus { get; set; }
        public bool IsPaymentApprove { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual Exhibitor Exhibitor { get; set; }
        public virtual Event Event { get; set; }
        public virtual Login Login { get; set; }
        public virtual BookingInstallment BookingInstallment { get; set; }

        public PaymentDetails()
        {
        }

        public PaymentDetails(double amountPaid, DateTime paymentDate, string paymentId, int paymentMode, string chequeNo, string bankName, string bankBranch, string utrNo, int paymentStatus, bool isPaymentApprove) : this()
        {
            Validate(amountPaid, paymentDate, paymentId, paymentMode, chequeNo, bankName,
                bankBranch,utrNo,paymentStatus,isPaymentApprove);

            this.AmountPaid = amountPaid;
            this.PaymentDate = paymentDate;
            this.PaymentID = paymentId;
            this.PaymentMode = paymentMode;
            this.ChequeNo = chequeNo;
            this.BankName = bankName;
            this.BankBranch = bankBranch;
            this.UTRNo = utrNo;
            this.PaymentStatus = paymentStatus;
            this.IsPaymentApprove = isPaymentApprove;
        }

        private static void Validate(double amountPaid, DateTime paymentDate, string paymentId, int paymentMode, string chequeNo, string bankName, string bankBranch, string utrNo, int paymentStatus, bool isPaymentApprove)
        {
            if (String.IsNullOrEmpty(paymentId))
                throw new ValidationException("Invalid PaymentId");
        }

        public static PaymentDetails Create(double amountPaid, DateTime paymentDate, string paymentId, int paymentMode, string chequeNo, string bankName, string bankBranch, string utrNo, int paymentStatus, bool isPaymentApprove)
        {
            return new PaymentDetails(amountPaid, paymentDate, paymentId, paymentMode, chequeNo, bankName, bankBranch, utrNo, paymentStatus, isPaymentApprove);
        }

        public void Update(double amountPaid, DateTime paymentDate, string paymentId, int paymentMode, string chequeNo, string bankName, string bankBranch, string utrNo, int paymentStatus, bool isPaymentApprove)
        {
            Validate(amountPaid, paymentDate, paymentId, paymentMode, chequeNo, bankName, bankBranch, utrNo, paymentStatus, isPaymentApprove);

            this.AmountPaid = amountPaid;
            this.PaymentDate = paymentDate;
            this.PaymentID = paymentId;
            this.PaymentMode = paymentMode;
            this.ChequeNo = chequeNo;
            this.BankName = bankName;
            this.BankBranch = bankBranch;
            this.UTRNo = utrNo;
            this.PaymentStatus = paymentStatus;
            this.IsPaymentApprove = isPaymentApprove;
        }

    }
}

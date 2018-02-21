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
    [Table("STALLTRANSACTION")]
    public class StallTransaction : MasterEntity
    {
        public string Status { get; set; }
        public double Amount { get; set; }
        public string PgTransactionNo { get; set; }
        public string IssuerRefNo { get; set; }
        public string AuthIdCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PgResponseCode { get; set; }
        public string AddressZip { get; set; }
        public string CitrusTxnId { get; set; }
        public string TxMsg { get; set; }
        public string PaymentMode { get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual Payment Payment { get; set; }

        public StallTransaction()
        {
        }

        private StallTransaction(string status, double amount, string pgTransactionNo, string issuerRefNo, string authIdCode, string firstName, string lastName, string pgResponseCode, string addressZip, string citrusTxnid, string paymentMode, DateTime transactionDate, string txMsg = null)
        {
            Validate(status, pgTransactionNo, issuerRefNo, authIdCode, firstName, lastName, pgResponseCode, addressZip, citrusTxnid, paymentMode, transactionDate, txMsg);

            this.Status = status;
            this.Amount = amount;
            this.PgTransactionNo = pgTransactionNo;
            this.IssuerRefNo = issuerRefNo;
            this.AuthIdCode = authIdCode;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PgResponseCode = pgResponseCode;
            this.AddressZip = addressZip;
            this.CitrusTxnId = citrusTxnid;
            this.PaymentMode = paymentMode;
            this.TransactionDate = transactionDate;
            this.TxMsg = txMsg;
        }

        private static void Validate(string status, string pgTransactionNo, string issuerRefNo, string authIdCode, string firstName, string lastName, string pgResponseCode, string addressZip, string citrusTxnid, string paymentMode, DateTime transactionDate, string txtMg)
        {
            if (String.IsNullOrEmpty(status))
                throw new ValidationException("Invalid status");
        }

        public static StallTransaction Create(string status, double amount, string pgTransactionNo, string issuerRefNo, string authIdCode, string firstName, string lastName, string pgResponseCode, string addressZip, string citrustxnId, string paymentMode, DateTime transactionDate, string txMsg = null)
        {
            return new StallTransaction(status, amount, pgTransactionNo, issuerRefNo, authIdCode, firstName, lastName, pgResponseCode, addressZip, citrustxnId, paymentMode, transactionDate, txMsg);
        }

        public void Update(string status, double amount, string pgTransactionNo, string issuerRefNo, string authIdCode, string firstName, string lastName, string pgResponseCode, string addressZip, string citrusId, string paymentMode, DateTime transactionDate, string txMsg = null)
        {
            Validate(status, pgTransactionNo, issuerRefNo, authIdCode, firstName, lastName, pgResponseCode, addressZip, citrusId, paymentMode, transactionDate, txMsg);

            this.Status = status;
            this.Amount = amount;
            this.PgTransactionNo = pgTransactionNo;
            this.IssuerRefNo = issuerRefNo;
            this.AuthIdCode = authIdCode;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PgResponseCode = pgResponseCode;
            this.AddressZip = addressZip;
            this.CitrusTxnId = citrusId;
        }
    }
}

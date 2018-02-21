using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("TRANSACTION")]
    public class Transaction : MasterEntity
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

        public virtual Ticket Ticket { get; set; }
        public virtual EventTicket EventTicket { get; set; }

        public Transaction()
        {
        }

        private Transaction(string status, double amount, string pgTransactionNo, string issuerRefNo, string authIdCode, string firstName, string lastName, string pgResponseCode, string addressZip, string citrusTxnid, string paymentMode, DateTime transactionDate, string txMsg = null)
        {
            Validate(status, pgTransactionNo, issuerRefNo, authIdCode, firstName, lastName, pgResponseCode, addressZip, citrusTxnid, paymentMode, transactionDate, txMsg);

            //this.TicketId = ticketId;
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
            //if (String.IsNullOrEmpty(ticketId))
            //    throw new ValidationException("Invalid transactionId");
            if (String.IsNullOrEmpty(status))
                throw new ValidationException("Invalid status");
            //if (String.IsNullOrEmpty(pgTransactionNo))
            //    throw new ValidationException("Invalid pgTransactionNo");
            //if (String.IsNullOrEmpty(issuerRefNo))
            //    throw new ValidationException("Invalid issuerRefNo");
            //if (String.IsNullOrEmpty(authIdCode))
            //    throw new ValidationException("Invalid authIdCode");
            //if (String.IsNullOrEmpty(firstName))
            //    throw new ValidationException("Invalid firstName");
            //if (String.IsNullOrEmpty(lastName))
            //    throw new ValidationException("Invalid lastName");
            //if (String.IsNullOrEmpty(addressZip))
            //    throw new ValidationException("Invalid addressZip");
            //if (String.IsNullOrEmpty(pgResponseCode))
            //    throw new ValidationException("Invalid pgResponseCode");
            //if (String.IsNullOrEmpty(citrusTxnid))
            //    throw new ValidationException("Citrus Txn id ");
        }

        public static Transaction Create(string status, double amount, string pgTransactionNo, string issuerRefNo, string authIdCode, string firstName, string lastName, string pgResponseCode, string addressZip, string citrustxnId, string paymentMode, DateTime transactionDate, string txMsg = null)
        {
            return new Transaction(status, amount, pgTransactionNo, issuerRefNo, authIdCode, firstName, lastName, pgResponseCode, addressZip, citrustxnId, paymentMode, transactionDate, txMsg);
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
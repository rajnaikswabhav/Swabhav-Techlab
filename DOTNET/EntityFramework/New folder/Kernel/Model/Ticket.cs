using Modules.EventManagement;
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
    [Table("TICKET")]
    public class Ticket : MasterEntity
    {
        public string TokenNumber { get; set; }
        public DateTime TicketDate { get; set; }
        public int NumberOfTicket { get; set; }
        public double TotalPriceOfTicket { get; set; }
        public string TxnIdForPaymentGateWay { get; set; }
        public bool PaymentCompleted { get; set; }
        public bool IsPayOnLocation { get; set; }
        public int ValidityDayCount { get; set; }
        public string Status { get; set; }

        public virtual Visitor Visitor { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual EventTicketType EventTicketType { get; set; }

        public Ticket()
        {
        }
        private Ticket(string TokenNumber, DateTime TicketDate, int NumberOfTicket, double TotalPriceOfTicket, string txnIdforPaymentGateWay)
        {
            this.TokenNumber = TokenNumber;
            this.TicketDate = TicketDate;
            this.NumberOfTicket = NumberOfTicket;
            this.TotalPriceOfTicket = TotalPriceOfTicket;
            this.TxnIdForPaymentGateWay = txnIdforPaymentGateWay;
        }

        public Ticket(string TokenNumber, string txnIdForPayments) : this()
        {
            Validate(TokenNumber, txnIdForPayments);

            this.TokenNumber = TokenNumber;
            this.TxnIdForPaymentGateWay = txnIdForPayments;
        }

        private static void Validate(string TokenNumber, string txnIdForPaymentGateWay)
        {
            if (String.IsNullOrEmpty(TokenNumber))
                throw new ValidationException("Invalid Category TokenNumber");

            if (String.IsNullOrEmpty(txnIdForPaymentGateWay))
                throw new ValidationException("Invalid TxnId For PaymentGateWay ");
        }

        public static Ticket Create(string TokenNumber, DateTime TicketDate, int NumberOfTicket, double TotalPriceOfTicket, string txnIdForPaymentGateWay)
        {
            return new Ticket(TokenNumber, TicketDate, NumberOfTicket, TotalPriceOfTicket, txnIdForPaymentGateWay);
        }

        public void Update(string TokenNumber, DateTime TicketDate, int NumberOfTicket, double TotalPriceOfTicket)
        {
            this.TokenNumber = TokenNumber;
            this.TicketDate = TicketDate;
            this.NumberOfTicket = NumberOfTicket;
            this.TotalPriceOfTicket = TotalPriceOfTicket;
        }

        /*

        _tokenGenerationService = new TokenGenerationService();

        public static Ticket Create()
        {
            var token = _tokenGenerationService.GenerateToken(ticketType);

            var ticket = new Ticket(token,...); // Create with Status PendingPayment

            if(paymentType == "AtLocation")
                _notificationService.Send(ticket, template)

            return ticket;
        }

        public void Pay(string mode, string transactionRef)
        {
            // Change status to Paid

            _notificationService.Send(ticket, template)

        }

        public void PayFail(string mode)
        {
            // Change statu to failed payment
        }
        */

        public bool IssueTicket()
        {
            //if (this.TicketDate < DateTime.UtcNow.Date ) {
            //    Status = "Expired";
            //    return false;
            //}
            if (this.ValidityDayCount == 0)
            {
                Status = "Used";
                return false;
            }

            ValidityDayCount = ValidityDayCount - 1;
            Status = "Issued";

            return true;

            // If 0 change statu to used
            // Send Notification of usage
        }
    }
}

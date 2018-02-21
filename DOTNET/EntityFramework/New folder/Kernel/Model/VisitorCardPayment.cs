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

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("VISITORCARDPAYMENT")]
    public class VisitorCardPayment : MasterEntity
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string ExhibitorName { get; set; }
        public string TransactionId { get; set; }
        public string Invoice { get; set; }
        public int Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual Event Event { get; set; }
        public virtual Login Login { get; set; }
        public virtual Exhibitor Exhibitor { get; set; }

        public VisitorCardPayment()
        {
        }

        public VisitorCardPayment(string name, string mobileNo, string exhibitorName, string transactionId, string invoice, int amount, DateTime transactionDate) : this()
        {
            Validate(name, mobileNo, exhibitorName, transactionId, invoice, amount, transactionDate);

            this.Name = name;
            this.MobileNo = mobileNo;
            this.ExhibitorName = exhibitorName;
            this.TransactionId = transactionId;
            this.Invoice = invoice;
            this.Amount = amount;
            this.TransactionDate = transactionDate;
        }

        private static void Validate(string name, string mobileNo, string exhibitorName, string transactionId, string invoice, int amount, DateTime transactionDate)
        {
            if (String.IsNullOrEmpty(mobileNo))
                throw new ValidationException("Invalid MobileNo");
        }

        public static VisitorCardPayment Create(string name, string mobileNo, string exhibitorName, string transactionId, string invoice, int amount, DateTime transactionDate)
        {
            return new VisitorCardPayment(name, mobileNo, exhibitorName, transactionId, invoice, amount, transactionDate);
        }

        public void Update(string name, string mobileNo, string exhibitorName, string transactionId, string invoice, int amount, DateTime transactionDate)
        {
            Validate(name, mobileNo, exhibitorName, transactionId, invoice, amount, transactionDate);

            this.Name = name;
            this.MobileNo = mobileNo;
            this.ExhibitorName = exhibitorName;
            this.TransactionId = transactionId;
            this.Invoice = invoice;
            this.Amount = amount;
            this.TransactionDate = transactionDate;
        }
    }
}

using Modules.EventManagement;
using Modules.LayoutManagement;
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
    [Table("VISITORPAYMENTDETAILS")]
    public class VisitorPaymentDetails : MasterEntity
    {
        public double Amount { get; set; }
        public string FileName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int PaymentMode { get; set; }
        public int PointsEarned { get; set; }
        public bool IsApproved { get; set; }

        public virtual Visitor Visitor { get; set; }
        public virtual Event Event { get; set; }
        public virtual Section Section { get; set; }
        public virtual Exhibitor Exhibitor { get; set; }

        public VisitorPaymentDetails()
        {
        }

        public VisitorPaymentDetails(double amount, string fileName, DateTime purchaseDate, int paymentMode, int pointsEarned, bool isApproved) : this()
        {
            Validate(amount, fileName, purchaseDate, paymentMode, pointsEarned, isApproved);

            this.Amount = amount;
            this.FileName = fileName;
            this.PurchaseDate = purchaseDate;
            this.PaymentMode = paymentMode;
            this.PointsEarned = pointsEarned;
            this.IsApproved = isApproved;
        }

        private static void Validate(double amount, string fileName, DateTime purchaseDate, int paymentMode, int pointsEarned, bool isApproved)
        {
            if (String.IsNullOrEmpty(fileName))
                throw new ValidationException("Invalid FileName");
        }

        public static VisitorPaymentDetails Create(double amount, string fileName, DateTime purchaseDate, int paymentMode, int pointsEarned, bool isApproved)
        {
            return new VisitorPaymentDetails(amount, fileName, purchaseDate, paymentMode, pointsEarned, isApproved);
        }

        public void Update(double amount, string fileName, DateTime purchaseDate, int paymentMode, int pointsEarned, bool isApproved)
        {
            Validate(amount, fileName, purchaseDate, paymentMode, pointsEarned, isApproved);

            this.Amount = amount;
            this.FileName = fileName;
            this.PurchaseDate = purchaseDate;
            this.PaymentMode = paymentMode;
            this.PointsEarned = pointsEarned;
            this.IsApproved = isApproved;
        }
    }
}

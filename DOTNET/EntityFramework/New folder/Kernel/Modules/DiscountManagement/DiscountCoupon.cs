using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Modules.DiscountManagement
{
    [Table("DISCOUNTCOUPON")]
    public class DiscountCoupon : MasterEntity
    {
        public string CouponCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DiscountText { get; set; }
        public int Discount { get; set; }
        public bool IsActive { get; set; }

        public virtual Event Event { get; set; }
        public virtual DiscountType DiscountType { get; set; }
        public DiscountCoupon()
        {
        }

        public DiscountCoupon(string couponCode, DateTime startDate, DateTime endDate, string discountText, int discount, bool isActive) : this()
        {
            Validate(couponCode, startDate, endDate, discountText, discount, isActive);

            this.CouponCode = couponCode;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.DiscountText = discountText;
            this.Discount = discount;
            this.IsActive = isActive;
        }

        private static void Validate(string couponCode, DateTime startDate, DateTime endDate, string discountText, int discount, bool isActive)
        {
            if (String.IsNullOrEmpty(couponCode))
                throw new ValidationException("Invalid Category Name");
        }

        public static DiscountCoupon Create(string couponCode, DateTime startDate, DateTime endDate, string discountText, int discount, bool isActive)
        {
            return new DiscountCoupon(couponCode, startDate, endDate, discountText, discount, isActive);
        }

        public void Update(string couponCode, DateTime startDate, DateTime endDate, string discountText, int discount, bool isActive)
        {
            Validate(couponCode, startDate, endDate, discountText, discount, isActive);

            this.CouponCode = couponCode;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.DiscountText = discountText;
            this.Discount = discount;
            this.IsActive = isActive;
        }

    }
}

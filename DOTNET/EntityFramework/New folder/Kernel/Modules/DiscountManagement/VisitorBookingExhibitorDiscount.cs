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

namespace Techlabs.Euphoria.Kernel.Modules.DiscountManagement
{
    [Table("VISITORBOOKINGEXHIBITORDISCOUNT")]
    public class VisitorBookingExhibitorDiscount : MasterEntity
    {
        public string DiscountcoupanCode { get; set; }

        public virtual Visitor Visitor { get; set; }
        public virtual BookingExhibitorDiscount BookingExhibitorDiscount { get; set; }

        public VisitorBookingExhibitorDiscount()
        {
        }

        public VisitorBookingExhibitorDiscount(string discountcoupanCode) : this()
        {
            Validate(discountcoupanCode);
            this.DiscountcoupanCode = discountcoupanCode;
        }

        private static void Validate(string discountcoupanCode)
        {
            if (String.IsNullOrEmpty(discountcoupanCode))
                throw new ValidationException("Invalid DiscountCouponCode");
        }

        public static VisitorBookingExhibitorDiscount Create(string discountcoupanCode)
        {
            return new VisitorBookingExhibitorDiscount(discountcoupanCode);
        }

        public void Update(string discountcoupanCode)
        {
            Validate(discountcoupanCode);
            this.DiscountcoupanCode = discountcoupanCode;
        }
    }
}

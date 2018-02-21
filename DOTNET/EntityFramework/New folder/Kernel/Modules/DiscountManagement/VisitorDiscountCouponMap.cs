using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.Modules.DiscountManagement
{
    [Table("VISITORDISCOUNTCOUPONMAP")]
    public class VisitorDiscountCouponMap : MasterEntity
    {
        public virtual Visitor Visitor { get; set; }
        public virtual DiscountCoupon DiscountCoupon { get; set; }
        public virtual EventTicket EventTicket { get; set; }
    }
}

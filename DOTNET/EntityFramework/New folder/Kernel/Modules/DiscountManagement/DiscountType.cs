using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Modules.DiscountManagement
{
    [Table("DISCOUNTTYPE")]
    public class DiscountType : MasterEntity
    {
        public string Type { get; set; }
    }
}

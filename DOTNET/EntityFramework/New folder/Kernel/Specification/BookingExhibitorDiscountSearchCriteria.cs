using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class BookingExhibitorDiscountSearchCriteria
    {
        public Guid EventId { get; set; }
        public Guid ProductId { get; set; }
        public Guid PavilionId { get; set; }
    }
}

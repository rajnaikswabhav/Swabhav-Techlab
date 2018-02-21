using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Modules.DiscountManagement;
using LinqKit;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class VisitorBookingExhibitorDiscountSpecificationForSearch : ISpecification<VisitorBookingExhibitorDiscount>
    {
        private VisitorBookingExhibitorDiscountSearchCriteria _criteria;

        public VisitorBookingExhibitorDiscountSpecificationForSearch(VisitorBookingExhibitorDiscountSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<VisitorBookingExhibitorDiscount, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<VisitorBookingExhibitorDiscount>();

                if (_criteria.VisitorId != Guid.Empty)
                    builder = builder.And(x => x.Visitor.Id.Equals(_criteria.VisitorId));

                if (_criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.BookingExhibitorDiscount.Booking.Event.Id.Equals(_criteria.EventId));

                if (_criteria.BookingExhibitorDiscountId != Guid.Empty)
                    builder = builder.And(x => x.BookingExhibitorDiscount.Id.Equals(_criteria.BookingExhibitorDiscountId));

                return builder;
            }
        }
    }
}

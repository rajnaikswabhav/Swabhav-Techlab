using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class BookingExhibitorDiscountSpecificationForSearch : ISpecification<BookingExhibitorDiscount>
    {
        private BookingExhibitorDiscountSearchCriteria _criteria;

        public BookingExhibitorDiscountSpecificationForSearch(BookingExhibitorDiscountSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<BookingExhibitorDiscount, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<BookingExhibitorDiscount>();

                if (_criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Booking.Event.Id.Equals(_criteria.EventId));

                if (_criteria.ProductId != Guid.Empty)
                    builder = builder.And(x => x.Booking.Exhibitor.Categories.Any(y => y.Id.Equals(_criteria.ProductId)));

                if (_criteria.PavilionId != Guid.Empty)
                    builder = builder.And(x => x.Booking.Section.Id.Equals(_criteria.PavilionId));

                return builder;
            }
        }
    }
}

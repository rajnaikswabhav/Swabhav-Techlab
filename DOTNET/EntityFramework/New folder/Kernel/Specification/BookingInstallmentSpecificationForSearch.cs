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
    public class BookingInstallmentSpecificationForSearch : ISpecification<BookingInstallment>
    {
        private BookingInstallmentSearchCriteria _criteria;

        public BookingInstallmentSpecificationForSearch(BookingInstallmentSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<BookingInstallment, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<BookingInstallment>();

                if (_criteria.BookingId != Guid.Empty)
                    builder = builder.And(x => x.Booking.Id.Equals(_criteria.BookingId));

                return builder;
            }
        }
    }
}

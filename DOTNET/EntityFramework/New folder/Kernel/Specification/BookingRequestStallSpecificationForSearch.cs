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
    public class BookingRequestStallSpecificationForSearch : ISpecification<BookingRequestStall>
    {
        private BookingRequestStallSearchCriteria _criteria;

        public BookingRequestStallSpecificationForSearch(BookingRequestStallSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<BookingRequestStall, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<BookingRequestStall>();

                if (_criteria.BookingRequestId != Guid.Empty && _criteria.Status == null)
                    builder = builder.And(x => x.BookingRequest.Id.Equals(_criteria.BookingRequestId));

                if (_criteria.EventId != Guid.Empty && _criteria.BookingRequestId == Guid.Empty && _criteria.Status != null)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.BookingRequest.Status.ToUpper() == _criteria.Status.ToUpper());

                return builder;
            }
        }
    }
}

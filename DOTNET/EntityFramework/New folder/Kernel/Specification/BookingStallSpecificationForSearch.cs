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
    public class BookingStallSpecificationForSearch : ISpecification<StallBooking>
    {
        private BookingStallSearchCriteria _criteria;

        public BookingStallSpecificationForSearch(BookingStallSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<StallBooking, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<StallBooking>();

                if (_criteria.BookingId != Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.Status == null)
                    builder = builder.And(x => x.Booking.Id.Equals(_criteria.BookingId)).And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.StallId != Guid.Empty && _criteria.EventId == Guid.Empty && _criteria.Status == null)
                    builder = builder.And(x => x.Stall.Id.Equals(_criteria.StallId));

                if (_criteria.StallId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.Status != null)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Booking.Status.ToUpper() == _criteria.Status.ToUpper());

                if (_criteria.StallId == Guid.Empty && _criteria.Status == null && _criteria.EventId != Guid.Empty && _criteria.CountryId != Guid.Empty)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Booking.Exhibitor.Country.Id.Equals(_criteria.CountryId));

                if (_criteria.StallId == Guid.Empty && _criteria.Status == null && _criteria.EventId != Guid.Empty && _criteria.CountryId == Guid.Empty && _criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue)
                {
                    TimeSpan ts = new TimeSpan(23, 59, 59);
                    DateTime EndDate = _criteria.EndDate.Date + ts;
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Booking.CreatedOn >= _criteria.StartDate && x.CreatedOn <= _criteria.EndDate);
                }
                return builder;
            }
        }
    }
}

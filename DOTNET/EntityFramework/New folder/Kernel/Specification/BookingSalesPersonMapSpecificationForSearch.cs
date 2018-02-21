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
    public class BookingSalesPersonMapSpecificationForSearch : ISpecification<BookingSalesPersonMap>
    {
        private BookingSalesPersonMapSearchCriteria _criteria;

        public BookingSalesPersonMapSpecificationForSearch(BookingSalesPersonMapSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<BookingSalesPersonMap, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<BookingSalesPersonMap>();

                if (_criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Booking.Event.Id.Equals(_criteria.EventId));

                if (_criteria.PavilionId != Guid.Empty)
                    builder = builder.And(x => x.Booking.Section.Id.Equals(_criteria.PavilionId));

                if (_criteria.ExhibitorIndustryId != Guid.Empty)
                    builder = builder.And(x => x.Booking.Exhibitor.ExhibitorIndustryType.Id.Equals(_criteria.ExhibitorIndustryId));

                if (_criteria.PartnerId != Guid.Empty)
                    builder = builder.And(x => x.Login.Partner.Id.Equals(_criteria.PartnerId));

                if (_criteria.EventId != Guid.Empty && _criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue)
                {
                    TimeSpan ts = new TimeSpan(23, 59, 59);
                    DateTime EndDate = _criteria.EndDate.Date + ts;
                    builder = builder.And(x => x.Booking.Event.Id.Equals(_criteria.EventId)).And(x => x.Booking.CreatedOn >= _criteria.StartDate && x.Booking.CreatedOn <= EndDate);
                }

                if (_criteria.BookingId != Guid.Empty && _criteria.SalesPersonId == Guid.Empty && _criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Booking.Id.Equals(_criteria.BookingId));

                if (_criteria.BookingId != Guid.Empty && _criteria.SalesPersonId == Guid.Empty && _criteria.EventId == Guid.Empty)
                    builder = builder.And(x => x.Booking.Id.Equals(_criteria.BookingId));

                if (_criteria.BookingId == Guid.Empty && _criteria.SalesPersonId != Guid.Empty && _criteria.EventId == Guid.Empty)
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.SalesPersonId));

                if (_criteria.BookingId == Guid.Empty && _criteria.SalesPersonId != Guid.Empty && _criteria.EventId != Guid.Empty && !string.IsNullOrEmpty(_criteria.ExhibitorName))
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.SalesPersonId)).And(x => x.Booking.Event.Id.Equals(_criteria.EventId)).And(x => x.Booking.Exhibitor.Name.Contains(_criteria.ExhibitorName) || x.Booking.Exhibitor.CompanyName.Contains(_criteria.ExhibitorName));

                if (_criteria.BookingId == Guid.Empty && _criteria.SalesPersonId != Guid.Empty && _criteria.EventId != Guid.Empty && string.IsNullOrEmpty(_criteria.ExhibitorName))
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.SalesPersonId)).And(x => x.Booking.Event.Id.Equals(_criteria.EventId));

                if (_criteria.BookingId == Guid.Empty && _criteria.SalesPersonId != Guid.Empty && _criteria.EventId != Guid.Empty && !string.IsNullOrEmpty(_criteria.ExhibitorName))
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.SalesPersonId)).And(x => x.Booking.Event.Id.Equals(_criteria.EventId)).And(x => x.Booking.Exhibitor.Name.Contains(_criteria.ExhibitorName) || x.Booking.Exhibitor.CompanyName.Contains(_criteria.ExhibitorName));

                return builder;
            }
        }
    }
}

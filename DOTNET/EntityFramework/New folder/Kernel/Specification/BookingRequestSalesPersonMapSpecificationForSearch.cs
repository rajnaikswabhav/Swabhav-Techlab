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
    public class BookingRequestSalesPersonMapSpecificationForSearch : ISpecification<BookingRequestSalesPersonMap>
    {
        private BookingRequestSalesPersonMapSearchCriteria _criteria;

        public BookingRequestSalesPersonMapSpecificationForSearch(BookingRequestSalesPersonMapSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<BookingRequestSalesPersonMap, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<BookingRequestSalesPersonMap>();

                if (_criteria.BookingRequestId != Guid.Empty && _criteria.SalesPersonId == Guid.Empty)
                    builder = builder.And(x => x.BookingRequest.Id.Equals(_criteria.BookingRequestId));

                if (_criteria.BookingRequestId == Guid.Empty && _criteria.EventId == Guid.Empty && _criteria.SalesPersonId != Guid.Empty && _criteria.Status == null)
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.SalesPersonId)).And(x => x.BookingRequest.Booking == null);

                if (_criteria.BookingRequestId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.SalesPersonId != Guid.Empty && _criteria.Status == null && string.IsNullOrEmpty(_criteria.ExhibitorCompanyName))
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.SalesPersonId)).And(x => x.BookingRequest.Booking == null).And(x => x.BookingRequest.Event.Id == _criteria.EventId);

                if (_criteria.BookingRequestId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.SalesPersonId != Guid.Empty && _criteria.Status == null && !string.IsNullOrEmpty(_criteria.ExhibitorCompanyName))
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.SalesPersonId)).And(x => x.BookingRequest.Booking == null).And(x => x.BookingRequest.Event.Id == _criteria.EventId)
                        .And(x => x.BookingRequest.Exhibitor.CompanyName.Contains(_criteria.ExhibitorCompanyName) || x.BookingRequest.Exhibitor.Name.Contains(_criteria.ExhibitorCompanyName));

                if (_criteria.BookingRequestId == Guid.Empty && _criteria.SalesPersonId != Guid.Empty && _criteria.Status != null)
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.SalesPersonId)).And(x => x.BookingRequest.Status.ToUpper() == _criteria.Status.ToUpper());

                return builder;
            }
        }
    }
}

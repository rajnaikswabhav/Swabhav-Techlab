using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Modules.PaymentManagement;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class BookingPaymentSpecificationForSearch : ISpecification<PaymentDetails>
    {
        private BookingPaymentSearchCriteria _criteria;

        public BookingPaymentSpecificationForSearch(BookingPaymentSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<PaymentDetails, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<PaymentDetails>();

                if (_criteria.BookingId != Guid.Empty && _criteria.SalesPersonId == Guid.Empty)
                    builder = builder.And(x => x.Booking.Id.Equals(_criteria.BookingId));

                if (_criteria.BookingId == Guid.Empty && _criteria.SalesPersonId != Guid.Empty && _criteria.EventId == Guid.Empty)
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.SalesPersonId));

                if (_criteria.BookingId == Guid.Empty && _criteria.SalesPersonId == Guid.Empty && _criteria.EventId != Guid.Empty && string.IsNullOrEmpty(_criteria.IsPaymentCompleted) && _criteria.PartnerId == Guid.Empty && !string.IsNullOrEmpty(_criteria.CompanyName))
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Exhibitor.CompanyName.Contains(_criteria.CompanyName));

                //if (_criteria.BookingId == Guid.Empty && _criteria.SalesPersonId == Guid.Empty && _criteria.EventId != Guid.Empty && string.IsNullOrEmpty(_criteria.IsPaymentCompleted) && _criteria.PartnerId == Guid.Empty && !string.IsNullOrEmpty(_criteria.CompanyName))
                //    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Exhibitor.Name.Contains(_criteria.CompanyName));

                if (_criteria.BookingId == Guid.Empty && _criteria.SalesPersonId == Guid.Empty && _criteria.EventId != Guid.Empty && string.IsNullOrEmpty(_criteria.IsPaymentCompleted) && _criteria.PartnerId == Guid.Empty && string.IsNullOrEmpty(_criteria.CompanyName))
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.BookingId == Guid.Empty && _criteria.SalesPersonId == Guid.Empty && _criteria.EventId != Guid.Empty && !string.IsNullOrEmpty(_criteria.IsPaymentCompleted) && _criteria.PartnerId == Guid.Empty)
                {
                    bool isApprove;
                    if (_criteria.IsPaymentCompleted.ToUpper().Equals("True".ToUpper()))
                    { isApprove = true; }
                    else { isApprove = false; }
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.IsPaymentApprove == isApprove);
                }
                if (_criteria.BookingId == Guid.Empty && _criteria.SalesPersonId != Guid.Empty && _criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Login.Id.Equals(_criteria.SalesPersonId));

                if (_criteria.BookingId == Guid.Empty && _criteria.SalesPersonId == Guid.Empty && _criteria.PartnerId != Guid.Empty && _criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Login.Partner.Id.Equals(_criteria.PartnerId));

                return builder;
            }
        }
    }
}

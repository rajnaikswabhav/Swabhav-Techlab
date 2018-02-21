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
    public class BookingRequestSpecificationForSearch : ISpecification<BookingRequest>
    {
        private BookingRequestSearchCriteria _criteria;

        public BookingRequestSpecificationForSearch(BookingRequestSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<BookingRequest, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<BookingRequest>();

                if (_criteria.EventId != Guid.Empty && _criteria.ExhibitorId == Guid.Empty && _criteria.Status == null)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.ExhibitorId != Guid.Empty && _criteria.EventId == Guid.Empty && _criteria.Status == null)
                    builder = builder.And(x => x.Exhibitor.Id.Equals(_criteria.ExhibitorId));

                if (_criteria.EventId != Guid.Empty && _criteria.ExhibitorId == Guid.Empty && _criteria.Status != null)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Status.ToUpper().Equals(_criteria.Status.ToUpper()));

                if (_criteria.EventId != Guid.Empty && _criteria.ExhibitorId == Guid.Empty && !string.IsNullOrEmpty(_criteria.BookingRequestId))
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.BookingRequestId.ToUpper().Equals(_criteria.BookingRequestId.ToUpper()));

                if (_criteria.EventId != Guid.Empty && !string.IsNullOrEmpty(_criteria.ExhibitorName) && _criteria.ExhibitorId == Guid.Empty && _criteria.Status != null)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.Status.ToUpper().Equals(_criteria.Status.ToUpper()))
                        .And(x => x.Exhibitor.Name.Contains(_criteria.ExhibitorName) || x.Exhibitor.CompanyName.Contains(_criteria.ExhibitorName));

                return builder;
            }
        }
    }
}

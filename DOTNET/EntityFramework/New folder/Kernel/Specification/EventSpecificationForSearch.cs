using LinqKit;
using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class EventSpecificationForSearch : ISpecification<Event>
    {
        private EventSearchCriteria _criteria;

        public EventSpecificationForSearch(EventSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<Event, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Event>();

                if (_criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Id.Equals(_criteria.EventId));

                if (_criteria.IsActive == true)
                    builder = builder.And(x => x.isActive.Equals(_criteria.IsActive));

                if (_criteria.VenueId != Guid.Empty && _criteria.IsTicketingActive == false)
                    builder = builder.And(x => x.Venue.Id.Equals(_criteria.VenueId));

                if (_criteria.VenueId != Guid.Empty && _criteria.IsTicketingActive != false)
                    builder = builder.And(x => x.Venue.Id.Equals(_criteria.VenueId)).And(x => x.IsTicketingActive == _criteria.IsTicketingActive);

                return builder;
            }
        }
    }
}

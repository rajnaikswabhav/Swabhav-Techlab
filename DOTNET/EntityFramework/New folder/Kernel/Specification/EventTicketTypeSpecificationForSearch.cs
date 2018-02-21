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
    public class EventTicketTypeSpecificationForSearch : ISpecification<EventTicketType>
    {
        private EventTicketTypeSearchCriteria _criteria;

        public EventTicketTypeSpecificationForSearch(EventTicketTypeSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<EventTicketType, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<EventTicketType>();

                if (_criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId));

                return builder;
            }
        }
    }
}

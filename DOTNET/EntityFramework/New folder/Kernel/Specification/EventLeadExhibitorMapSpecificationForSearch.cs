using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Modules.LeadGeneration;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class EventLeadExhibitorMapSpecificationForSearch : ISpecification<EventLeadExhibitorMap>
    {
        private EventLeadExhibitorMapSearchCriteria _criteria;

        public EventLeadExhibitorMapSpecificationForSearch(EventLeadExhibitorMapSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<EventLeadExhibitorMap, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<EventLeadExhibitorMap>();

                if (_criteria.SalesPersonId != Guid.Empty && _criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.SalesPersonId)).And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.ExhibitorId != Guid.Empty && _criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Exhibitor.Id.Equals(_criteria.ExhibitorId)).And(x => x.Event.Id.Equals(_criteria.EventId));

                return builder;
            }
        }
    }
}

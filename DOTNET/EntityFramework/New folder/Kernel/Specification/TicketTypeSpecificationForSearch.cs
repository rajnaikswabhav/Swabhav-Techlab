using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.Specification
{
   public class TicketTypeSpecificationForSearch : ISpecification<TicketType>
    {
        private TicketTypeSearchCriteria _criteria;

        public TicketTypeSpecificationForSearch(TicketTypeSearchCriteria criteria)
        {
            _criteria = criteria;
        }
        public Expression<Func<TicketType, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<TicketType>();

                if (!String.IsNullOrEmpty(_criteria.VenueId))
                    builder = builder.And(x => x.Venue.Id.ToString().Equals(_criteria.VenueId));

                return builder;
            }
        }
    }
}

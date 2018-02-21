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
    public class VenueSpecificationForSearch : ISpecification<Venue>
    {
        private VenueSearchCriteria _criteria;

        public VenueSpecificationForSearch(VenueSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<Venue, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Venue>();

                if (!String.IsNullOrEmpty(_criteria.VenueId))
                    builder = builder.And(x => x.Id.Equals(new Guid (_criteria.VenueId)));

                return builder;
            }
        }
    }
}

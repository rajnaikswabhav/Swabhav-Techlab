using LinqKit;
using Modules.LayoutManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class StallSpecificationForSearch : ISpecification<Stall>
    {
        private StallSearchCriteria _criteria;

        public StallSpecificationForSearch(StallSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<Stall, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Stall>();

                if (_criteria.sectionId != Guid.Empty && _criteria.EventId == Guid.Empty && _criteria.IsBooked == null && _criteria.StallSize == null)
                    builder = builder.And(x => x.Section.Id.Equals(_criteria.sectionId));

                if (_criteria.sectionId != Guid.Empty && _criteria.EventId == Guid.Empty && _criteria.IsBooked != null && _criteria.StallSize == null)
                    builder = builder.And(x => x.Section.Id.Equals(_criteria.sectionId)).And(x => x.IsBooked == false);

                if (_criteria.sectionId != Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.StallSize == null)
                    builder = builder.And(x => x.Section.Id.Equals(_criteria.sectionId)).And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.sectionId == Guid.Empty && _criteria.EventId != Guid.Empty && _criteria.StallSize != null)
                    builder = builder.And(x => x.StallSize == _criteria.StallSize).And(x => x.Event.Id.Equals(_criteria.EventId));

                return builder;
            }
        }
    }
}

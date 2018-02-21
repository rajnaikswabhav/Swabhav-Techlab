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
    public class LayoutPlanSpecificationForSearch : ISpecification<LayoutPlan>
    {
        private LayoutPlanSearchCriteria _criteria;

        public LayoutPlanSpecificationForSearch(LayoutPlanSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<LayoutPlan, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<LayoutPlan>();

                if (_criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId));

                return builder;
            }
        }
    }
}

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
    public class BarrierSpecificationForSearch : ISpecification<Barrier>
    {
        private BarrierSearchCriteria _criteria;

        public BarrierSpecificationForSearch(BarrierSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<Barrier, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Barrier>();

                if (_criteria.SectionId != Guid.Empty)
                    builder = builder.And(x => x.Section.Id.Equals(_criteria.SectionId));

                return builder;
            }
        }
    }
}

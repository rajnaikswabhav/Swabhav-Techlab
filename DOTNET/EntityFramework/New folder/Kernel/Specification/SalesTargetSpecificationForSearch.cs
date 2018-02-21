using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Modules.LayoutManagement;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class SalesTargetSpecificationForSearch: ISpecification<SalesTarget>
    {
        private SalesTargetSearchCriteria _criteria;

        public SalesTargetSpecificationForSearch(SalesTargetSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<SalesTarget, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<SalesTarget>();

                if (_criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.SalesPersonId != Guid.Empty)
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.SalesPersonId));

                return builder;
            }
        }
    }
}

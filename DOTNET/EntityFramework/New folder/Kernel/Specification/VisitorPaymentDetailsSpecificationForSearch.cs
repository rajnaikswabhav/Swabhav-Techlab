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
    public class VisitorPaymentDetailsSpecificationForSearch : ISpecification<VisitorPaymentDetails>
    {
        private VisitorPaymentDetailsSearchCriteria _criteria;

        public VisitorPaymentDetailsSpecificationForSearch(VisitorPaymentDetailsSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<VisitorPaymentDetails, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<VisitorPaymentDetails>();

                if (_criteria.EventId != Guid.Empty )
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId));

                return builder;
            }
        }
    }
}

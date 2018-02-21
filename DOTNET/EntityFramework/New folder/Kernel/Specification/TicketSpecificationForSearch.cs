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
    public class TicketSpecificationForSearch : ISpecification<Ticket>
    {
        private TicketSearchCriteria _criteria;

        public TicketSpecificationForSearch(TicketSearchCriteria criteria)
        {
            _criteria = criteria;
        }
        public Expression<Func<Ticket, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Ticket>();

                if (!String.IsNullOrEmpty(_criteria.VisitorId))
                    builder = builder.And(x => x.Visitor.Id.Equals(_criteria.VisitorId));

                if (!String.IsNullOrEmpty(_criteria.TokenNo))
                    builder = builder.And(x => x.TokenNumber == _criteria.TokenNo);

                return builder;
            }
        }
    }
}

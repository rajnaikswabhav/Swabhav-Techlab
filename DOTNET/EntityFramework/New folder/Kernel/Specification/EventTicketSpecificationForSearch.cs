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
    public class EventTicketSpecificationForSearch : ISpecification<EventTicket>
    {
        private EventTicketSearchCriteria _criteria;

        public EventTicketSpecificationForSearch(EventTicketSearchCriteria criteria)
        {
            _criteria = criteria;
        }
        public Expression<Func<EventTicket, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<EventTicket>();

                if (_criteria.VisitorId != Guid.Empty)
                    builder = builder.And(x => x.Visitor.Id.Equals(_criteria.VisitorId)).And(x => x.PaymentCompleted == true);

                if (!String.IsNullOrEmpty(_criteria.TokenNo))
                    builder = builder.And(x => x.TokenNumber.Contains(_criteria.TokenNo));

                return builder;
            }
        }
    }
}

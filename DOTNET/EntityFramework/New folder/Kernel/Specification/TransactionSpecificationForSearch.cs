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
    public class TransactionSpecificationForSearch : ISpecification<Transaction>
    {
        private TransactionSearchCriteria _criteria;

        public TransactionSpecificationForSearch(TransactionSearchCriteria criteria)
        {
            _criteria = criteria;
        }
        public Expression<Func<Transaction, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Transaction>();

                if (!String.IsNullOrEmpty(_criteria.TicketId.ToString()))
                    builder = builder.And(x => x.EventTicket.Id.Equals(_criteria.TicketId));

                return builder;
            }
        }
    }
}

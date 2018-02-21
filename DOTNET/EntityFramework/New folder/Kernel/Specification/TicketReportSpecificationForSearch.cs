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
    public class TicketReportSpecificationForSearch : ISpecification<Ticket>
    {
        TicketReportSearchCriteria _criteria;

        public TicketReportSpecificationForSearch(TicketReportSearchCriteria criteria)
        {

            _criteria = criteria;

        }
        public Expression<Func<Ticket, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Ticket>();


                if (_criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue)
                {
                    builder = builder.And(x => x.TicketDate >= _criteria.StartDate && x.TicketDate <= _criteria.EndDate);
                    //builder = builder.And(x => x.TicketDate.Day >= _criteria.StartDate.Day || x.TicketDate.Day <= _criteria.EndDate.Day);
                    //builder = builder.And(x => x.TicketDate.Month >= _criteria.StartDate.Month && x.TicketDate.Month >= _criteria.EndDate.Month);
                    //builder = builder.And(x => x.TicketDate.Year >= _criteria.StartDate.Year && x.TicketDate.Year >= _criteria.EndDate.Year);
                }

                return builder;
            }
        }
    }
}

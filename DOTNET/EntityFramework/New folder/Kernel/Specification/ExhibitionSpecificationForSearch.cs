using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class ExhibitionSpecificationForSearch : ISpecification<Exhibition>
    {
         ExhibitionSearchCriteria _criteria;

        public ExhibitionSpecificationForSearch(ExhibitionSearchCriteria criteria)
        {
            _criteria = criteria;
        }
        public Expression<Func<Exhibition, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Exhibition>();

                if (_criteria.TicketDate != DateTime.MinValue)
                {
                    builder = builder.And(x => x.StartDate.Day <= _criteria.TicketDate.Day || x.EndDate.Day >= _criteria.TicketDate.Day);
                    builder = builder.And(x => x.StartDate.Month <= _criteria.TicketDate.Month && x.EndDate.Month >= _criteria.TicketDate.Month);
                    builder = builder.And(x => x.StartDate.Year <= _criteria.TicketDate.Year && x.EndDate.Year >= _criteria.TicketDate.Year);
                }                    

                if (_criteria.ExhibitionId != Guid.Empty)
                    builder = builder.And(x => x.Id.Equals(_criteria.ExhibitionId));

                return builder;
            }
        }

    }
}

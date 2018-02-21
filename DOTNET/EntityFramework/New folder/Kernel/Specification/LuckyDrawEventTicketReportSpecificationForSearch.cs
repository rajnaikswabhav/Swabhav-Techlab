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
    public class LuckyDrawEventTicketReportSpecificationForSearch : ISpecification<LuckyDrawEventTicket>
    {
        LuckyDrawEventTicketReportSearchCriteria _criteria;

        public LuckyDrawEventTicketReportSpecificationForSearch(LuckyDrawEventTicketReportSearchCriteria criteria)
        {

            _criteria = criteria;

        }
        public Expression<Func<LuckyDrawEventTicket, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<LuckyDrawEventTicket>();


                if (_criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue && _criteria.AdminId == Guid.Empty)
                {
                    builder = builder.And(x => x.TicketDate >= _criteria.StartDate && x.TicketDate <= _criteria.EndDate).And(x => x.CreatedBy != null);
                    //builder = builder.And(x => x.TicketDate.Day >= _criteria.StartDate.Day || x.TicketDate.Day <= _criteria.EndDate.Day);
                    //builder = builder.And(x => x.TicketDate.Month >= _criteria.StartDate.Month && x.TicketDate.Month >= _criteria.EndDate.Month);
                    //builder = builder.And(x => x.TicketDate.Year >= _criteria.StartDate.Year && x.TicketDate.Year >= _criteria.EndDate.Year);
                }

                if (_criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue && _criteria.AdminId != Guid.Empty)
                {
                    builder = builder.And(x => x.TicketDate >= _criteria.StartDate && x.TicketDate <= _criteria.EndDate).And(x => x.CreatedBy == _criteria.AdminId);
                }

                if (_criteria.DateForSearch != DateTime.MinValue && _criteria.StartTime != 0 && (_criteria.EndTime != null && _criteria.EndTime != 0) && _criteria.AdminId != Guid.Empty)
                {
                    builder = builder.And(x => x.CreatedOn.Day == _criteria.DateForSearch.Day).And(x => x.CreatedBy == _criteria.AdminId).And(x => x.CreatedOn.Hour >= _criteria.StartTime && x.CreatedOn.Hour <= _criteria.EndTime);
                }
                if (_criteria.DateForSearch != DateTime.MinValue && _criteria.StartDate == DateTime.MinValue && _criteria.StartTime == 0 && _criteria.EndDate == DateTime.MinValue && _criteria.AdminId != Guid.Empty)
                {
                    builder = builder.And(x => x.CreatedOn.Day == _criteria.DateForSearch.Day).And(x => x.CreatedBy == _criteria.AdminId);
                }
                return builder;
            }
        }
    }
}

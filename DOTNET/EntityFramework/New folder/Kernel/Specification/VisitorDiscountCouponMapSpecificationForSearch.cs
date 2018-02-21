using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Modules.DiscountManagement;
using LinqKit;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class VisitorDiscountCouponMapSpecificationForSearch : ISpecification<VisitorDiscountCouponMap>
    {
        private VisitorDiscountCouponMapSearchCriteria _criteria;

        public VisitorDiscountCouponMapSpecificationForSearch(VisitorDiscountCouponMapSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<VisitorDiscountCouponMap, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<VisitorDiscountCouponMap>();

                if (_criteria.VisitorId != Guid.Empty)
                    builder = builder.And(x => x.Visitor.Id.Equals(_criteria.VisitorId));

                if (_criteria.DiscountCouponId != Guid.Empty)
                    builder = builder.And(x => x.DiscountCoupon.Id.Equals(_criteria.DiscountCouponId));

                if (_criteria.EventTicketId != Guid.Empty)
                    builder = builder.And(x => x.EventTicket.Id.Equals(_criteria.EventTicketId));

                if (_criteria.StartDate != DateTime.MinValue && _criteria.EndDate != DateTime.MinValue)
                {
                    TimeSpan ts = new TimeSpan(00, 00, 00);
                    DateTime StartDateForSearch = _criteria.StartDate.Date + ts;

                    TimeSpan timeSpan = new TimeSpan(23, 59, 59);
                    DateTime EndDateForSearch = _criteria.EndDate.Date + timeSpan;

                    builder = builder.And(x => x.EventTicket.TicketDate >= _criteria.StartDate && x.EventTicket.TicketDate <= _criteria.EndDate).And(x => x.EventTicket.PaymentCompleted).And(x => x.CreatedBy == null);
                }
                return builder;
            }
        }
    }
}

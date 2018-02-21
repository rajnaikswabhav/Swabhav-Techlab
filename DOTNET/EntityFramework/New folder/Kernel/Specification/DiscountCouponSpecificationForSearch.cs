using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Modules.DiscountManagement;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class DiscountCouponSpecificationForSearch : ISpecification<DiscountCoupon>
    {
        private DiscountCouponSearchCriteria _criteria;

        public DiscountCouponSpecificationForSearch(DiscountCouponSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<DiscountCoupon, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<DiscountCoupon>();

                if (_criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId));

                if (!String.IsNullOrEmpty(_criteria.CouponCode))
                    builder = builder.And(x => x.CouponCode.ToUpper().Equals(_criteria.CouponCode.ToUpper()));

                if (_criteria.Date != DateTime.MinValue)
                    builder = builder.And(x => x.StartDate <= _criteria.Date && x.EndDate >= _criteria.Date);

                return builder;
            }
        }
    }
}

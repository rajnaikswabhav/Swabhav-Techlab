using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Modules.PaymentManagement;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class PaymentSpecificationForSearch : ISpecification<Payment>
    {
        private PaymentSearchCriteria _criteria;

        public PaymentSpecificationForSearch(PaymentSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<Payment, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Payment>();

                if (_criteria.BookingId != Guid.Empty)
                    builder = builder.And(x => x.Booking.Id.Equals(_criteria.BookingId)).And(x=>x.IsPaymentDone==true);

                return builder;
            }
        }
    }
}

using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Modules.BookingManagement;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class BookingRequestInstallmentSpecificationForSearch : ISpecification<BookingRequestInstallment>
    {
        private BookingRequestInstallmentSearchCriteria _criteria;

        public BookingRequestInstallmentSpecificationForSearch(BookingRequestInstallmentSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<BookingRequestInstallment, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<BookingRequestInstallment>();

                if (_criteria.BookingRequestId != Guid.Empty)
                    builder = builder.And(x => x.BookingRequest.Id.Equals(_criteria.BookingRequestId));

                return builder;
            }
        }
    }
}

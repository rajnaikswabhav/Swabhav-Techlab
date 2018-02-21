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
    public class VisitorCardPaymentSpecificationForSearch : ISpecification<VisitorCardPayment>
    {
        private VisitorCardPaymentSearchCriteria _criteria;

        public VisitorCardPaymentSpecificationForSearch(VisitorCardPaymentSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<VisitorCardPayment, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<VisitorCardPayment>();

                if (_criteria.EventId != Guid.Empty)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId));

                if (_criteria.LoginId != Guid.Empty)
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.LoginId));

                if (_criteria.MobileNo != null)
                    builder = builder.And(x => x.MobileNo == _criteria.MobileNo);

                if (_criteria.EventId != Guid.Empty && _criteria.ExhibitorName != null && _criteria.MobileNo == null)
                    builder = builder.And(x => x.Event.Id.Equals(_criteria.EventId)).And(x => x.ExhibitorName == _criteria.ExhibitorName);

                if (_criteria.ExhibitorId != Guid.Empty)
                    builder = builder.And(x => x.Exhibitor.Id.Equals(_criteria.ExhibitorId));

                if (_criteria.CardPaymentDate != DateTime.MinValue)
                {
                    TimeSpan ts = new TimeSpan(00, 00, 00);
                    DateTime StartDateForSearch = _criteria.CardPaymentDate.Date + ts;

                    TimeSpan timeSpan = new TimeSpan(23, 59, 59);
                    DateTime EndDateForSearch = _criteria.CardPaymentDate.Date + timeSpan;
                    builder = builder.And(x => x.TransactionDate >= StartDateForSearch && x.TransactionDate <= EndDateForSearch);
                }
                return builder;
            }
        }
    }
}

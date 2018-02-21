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
   public class VisitorSpecificationForSearch : ISpecification<Visitor>
    {
        private VisitorSearchCriteria _criteria;

        public VisitorSpecificationForSearch(VisitorSearchCriteria criteria)
        {

            _criteria = criteria;

        }
        public Expression<Func<Visitor, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Visitor>();
                
                if (!string.IsNullOrEmpty(_criteria.EmailId) )
                    builder = builder.And(x => x.EmailId.ToUpper().Equals(_criteria.EmailId.ToUpper()));

                if (!string.IsNullOrEmpty(_criteria.PhoneNumber))
                    builder = builder.And(x => x.MobileNo.Equals(_criteria.PhoneNumber));

                return builder;
            }
        }
    }
}

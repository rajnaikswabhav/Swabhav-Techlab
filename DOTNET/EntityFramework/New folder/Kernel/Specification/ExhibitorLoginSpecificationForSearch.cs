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
    public class ExhibitorLoginSpecificationForSearch : ISpecification<Exhibitor>
    {
        private ExhibitorLoginSearchCriteria _criteria;

        public ExhibitorLoginSpecificationForSearch(ExhibitorLoginSearchCriteria criteria)
        {
            _criteria = criteria;
        }
        public Expression<Func<Exhibitor, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Exhibitor>();

                if (!string.IsNullOrEmpty(_criteria.EmailId) && !string.IsNullOrEmpty(_criteria.Password))
                    builder = builder.And(x => x.Password.Equals(_criteria.Password)).And(x => x.EmailId.Equals(_criteria.EmailId));

                if(!string.IsNullOrEmpty(_criteria.ExhibitorName) && !string.IsNullOrEmpty(_criteria.EmailId))
                    builder = builder.And(x => x.Name.ToUpper().Equals(_criteria.ExhibitorName.ToUpper())).And(x => x.EmailId.ToUpper().Equals(_criteria.EmailId.ToUpper()));

                return builder;
            }
        }
    }
}

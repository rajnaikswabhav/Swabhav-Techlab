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
    public class ExhibitorTypeSpecificationForSearch : ISpecification<ExhibitorType>
    {
        private ExhibitorTypeSearchCriteria _criteria;

        public ExhibitorTypeSpecificationForSearch(ExhibitorTypeSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<ExhibitorType, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<ExhibitorType>();

                if (string.IsNullOrEmpty(_criteria.ExhibitorType))
                    builder = builder.And(x => x.Type.ToUpper().Equals(_criteria.ExhibitorType.ToUpper()));

                return builder;
            }
        }
    }
}
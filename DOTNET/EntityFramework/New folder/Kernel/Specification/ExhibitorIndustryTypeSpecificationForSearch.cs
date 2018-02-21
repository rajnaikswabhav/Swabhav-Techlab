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
    public class ExhibitorIndustryTypeSpecificationForSearch : ISpecification<ExhibitorIndustryType>
    {
        private ExhibitorIndustryTypeSearchCriteria _criteria;

        public ExhibitorIndustryTypeSpecificationForSearch(ExhibitorIndustryTypeSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<ExhibitorIndustryType, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<ExhibitorIndustryType>();

                if (!string.IsNullOrEmpty(_criteria.IndustryType))
                    builder = builder.And(x => x.IndustryType.ToUpper().Equals(_criteria.IndustryType.ToUpper()));

                return builder;
            }
        }
    }
}

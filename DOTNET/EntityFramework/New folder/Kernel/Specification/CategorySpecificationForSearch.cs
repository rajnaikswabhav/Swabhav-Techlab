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
    public class CategorySpecificationForSearch : ISpecification<Category>
    {
        private CategorySearchCriteria _criteria;

        public CategorySpecificationForSearch(CategorySearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<Category, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Category>();

                if (!String.IsNullOrEmpty(_criteria.Name))
                    builder = builder.And(x => x.Name.ToUpper().Equals(_criteria.Name.ToUpper()));

                return builder;
            }
        }
    }
}

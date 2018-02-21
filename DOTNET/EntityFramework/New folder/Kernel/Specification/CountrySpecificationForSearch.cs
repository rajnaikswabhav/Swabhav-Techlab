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
    public class CountrySpecificationForSearch : ISpecification<Country>
    {
        private CountrySearchCriteria _criteria;

        public CountrySpecificationForSearch(CountrySearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<Country, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Country>();

                if (!String.IsNullOrEmpty(_criteria.ExhibitionId) && _criteria.CountryName == null)
                    builder = builder.And(x => x.Exhibitons.Id.ToString().Equals(_criteria.ExhibitionId));

                if (String.IsNullOrEmpty(_criteria.ExhibitionId) && _criteria.CountryName != null)
                    builder = builder.And(x => x.Name.ToUpper().Equals(_criteria.CountryName.ToUpper()));

                return builder;
            }
        }
    }
}

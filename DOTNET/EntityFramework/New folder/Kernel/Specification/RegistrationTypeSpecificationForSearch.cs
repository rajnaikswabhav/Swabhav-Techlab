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
    public class RegistrationTypeSpecificationForSearch : ISpecification<ExhibitorRegistrationType>
    {
        private RegistrationTypeSearchCriteria _criteria;

        public RegistrationTypeSpecificationForSearch(RegistrationTypeSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<ExhibitorRegistrationType, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<ExhibitorRegistrationType>();

                if (!string.IsNullOrEmpty(_criteria.RegistrationType))
                    builder = builder.And(x => x.RegistrationType.Equals(_criteria.RegistrationType));

                return builder;
            }
        }
    }
}

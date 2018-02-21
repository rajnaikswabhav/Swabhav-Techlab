using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class ExhibitorEnquirySpecificationForSearch : ISpecification<ExhibitorEnquiry>
    {
        ExhibitorEnquirySearchCriteria _criteria;

        public ExhibitorEnquirySpecificationForSearch(ExhibitorEnquirySearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public System.Linq.Expressions.Expression<Func<ExhibitorEnquiry, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<ExhibitorEnquiry>();

                if (_criteria.ExhibitorRegistrationTypeId != Guid.Empty)
                {
                    builder = builder.And(x => x.ExhibitorRegistrationType.Id.Equals(_criteria.ExhibitorRegistrationTypeId));
                }
                if (!string.IsNullOrEmpty(_criteria.ExhibitorRegistrationType))
                {
                    if (_criteria.ExhibitorRegistrationType == "true")
                        builder = builder.And(x => x.ExhibitorRegistrationType != null);
                }
                return builder;
            }
        }
    }
}

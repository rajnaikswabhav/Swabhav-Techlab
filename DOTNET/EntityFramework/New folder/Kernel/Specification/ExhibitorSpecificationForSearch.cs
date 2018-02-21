using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class ExhibitorSpecificationForSearch : ISpecification<Exhibitor>
    {
        ExhibitorSearchCriteria _criteria;

        public ExhibitorSpecificationForSearch(ExhibitorSearchCriteria critera)
        {
            _criteria = critera;
        }

        public System.Linq.Expressions.Expression<Func<Exhibitor, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Exhibitor>();

                if (!String.IsNullOrEmpty(_criteria.CompanyName))
                {
                    builder = builder.And(x => x.CompanyName.Contains(_criteria.CompanyName) || x.Name.Contains(_criteria.CompanyName));
                }

                if (!String.IsNullOrEmpty(_criteria.Category))
                {
                    builder = builder.And(x => x.Categories.Any(y => y.Name.Contains(_criteria.Category)));
                }

                if (!String.IsNullOrEmpty(_criteria.Country))
                {
                    builder = builder.And(x => x.Country.Name.Contains(_criteria.Country));
                }
                if (_criteria.CountryId != Guid.Empty)
                {
                    builder = builder.And(x => x.Country.Id.Equals(_criteria.CountryId));
                }

                if (!String.IsNullOrEmpty(_criteria.State))
                {
                    builder = builder.And(x => x.State.Name.Contains(_criteria.State));
                }

                if (!String.IsNullOrEmpty(_criteria.Pavilion))
                {
                    builder = builder.And(x => x.Stalls.Any(y => y.Pavilion.Name.Contains(_criteria.Pavilion)));
                }

                if (_criteria.StartRange != 0 && _criteria.StartRange != null && _criteria.EndRange != 0 && _criteria.EndRange != null)
                {
                    if (_criteria.EndRange == 11)
                    {
                        builder = builder.And(x => x.Age >= 10);

                    }
                    else if (_criteria.StartRange == 1)
                    {
                        int startRange = _criteria.StartRange - 1;
                        builder = builder.And(x => x.Age >= startRange && x.Age < _criteria.EndRange);
                    }
                    else
                    {
                        builder = builder.And(x => x.Age >= _criteria.StartRange && x.Age < _criteria.EndRange);
                    }
                }

                if (_criteria.ExhibitorTypeId != Guid.Empty && _criteria.StartRange == 0 && _criteria.EndRange == 0)
                {
                    builder = builder.And(x => x.ExhibitorType.Id.Equals(_criteria.ExhibitorTypeId));
                }

                if (_criteria.ExhibitorIndustryTypeId != Guid.Empty && _criteria.ExhibitorTypeId == Guid.Empty && _criteria.StartRange == 0 && _criteria.EndRange == 0)
                {
                    builder = builder.And(x => x.ExhibitorIndustryType.Id.Equals(_criteria.ExhibitorIndustryTypeId));
                }
                if (_criteria.ExhibitorRegistrationTypeId != Guid.Empty)
                {
                    builder = builder.And(x => x.ExhibitorRegistrationType.Id.Equals(_criteria.ExhibitorRegistrationTypeId));
                }
                if (!string.IsNullOrEmpty(_criteria.ExhibitorRegistrationType))
                {
                    if (_criteria.ExhibitorRegistrationType == "true")
                        builder = builder.And(x => x.ExhibitorRegistrationType.RegistrationType != "self").And(x => x.ExhibitorRegistrationType != null);
                }

                return builder;
            }
        }
    }
}

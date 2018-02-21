using LinqKit;
using Modules.LayoutManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;

namespace Techlabs.Euphoria.Kernel.Specification
{
   public class SectionSpecificationForSearch : ISpecification<Section>
    {
        private SectionSearchCriteria _criteria;

        public SectionSpecificationForSearch(SectionSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<Section, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Section>();

                if (_criteria.LayoutId != Guid.Empty && _criteria.Type == null)
                    builder = builder.And(x => x.LayoutPlan.Id.Equals(_criteria.LayoutId));

                if (_criteria.LayoutId != Guid.Empty && _criteria.Type != null)
                    builder = builder.And(x => x.LayoutPlan.Id.Equals(_criteria.LayoutId)).And(x=>x.SectionType.Name==_criteria.Type);

                return builder;
            }
        }
    }
}

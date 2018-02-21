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
    public class GeneralMasterSpecificationForSearch : ISpecification<GeneralMaster>
    {
        private GeneralMasterSearchCriteria _criteria;

        public GeneralMasterSpecificationForSearch(GeneralMasterSearchCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<GeneralMaster, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<GeneralMaster>();

                if (!string.IsNullOrEmpty(_criteria.GeneralTableType))
                    builder = builder.And(x => x.Type.Equals(_criteria.GeneralTableType));

                return builder;
            }
        }
    }
}

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
    public class StateSpecificationForSearch : ISpecification<State>
    {
        private StateSearchCriteria _criteria;

        public StateSpecificationForSearch(StateSearchCriteria criteria)
        {

            _criteria = criteria;

        }
        public Expression<Func<State, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<State>();


                if (!String.IsNullOrEmpty(_criteria.ExhibitionId))
                    builder = builder.And(x => x.Exhibitons.Id.ToString().Equals(_criteria.ExhibitionId));


                return builder;
            }
        }
    }
}

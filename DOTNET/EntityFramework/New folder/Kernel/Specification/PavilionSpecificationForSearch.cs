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
   public class PavilionSpecificationForSearch : ISpecification<Pavilion>
    {
        private PavilionSearchCriteria _criteria;

        public PavilionSpecificationForSearch(PavilionSearchCriteria criteria) {

            _criteria = criteria;

        }
        public Expression<Func<Pavilion, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Pavilion>();

                
                if (!String.IsNullOrEmpty(_criteria.ExhibitionId))
                  builder=  builder.And(x => x.Exhibition.Id.ToString().Equals(_criteria.ExhibitionId));
                   // builder = builder.And(x => false);


                return builder;
            }
        }
    }
}

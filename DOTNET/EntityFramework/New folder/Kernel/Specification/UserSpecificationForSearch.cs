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
    public class UserSpecificationForSearch : ISpecification<User>
    {
        private UserSearchCriteria _criteria;

        public UserSpecificationForSearch(UserSearchCriteria criteria)
        {

            _criteria = criteria;

        }
        public System.Linq.Expressions.Expression<Func<User, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<User>();


                if (!String.IsNullOrEmpty(_criteria.Name))
                {
                    builder = builder.And(x => x.UserName.Equals(_criteria.Name));

                }

                if (!String.IsNullOrEmpty(_criteria.Password))
                {
                    builder = builder.And(x => x.Password.Equals(_criteria.Password));

                }


                return builder;
            }
        }
    }
}

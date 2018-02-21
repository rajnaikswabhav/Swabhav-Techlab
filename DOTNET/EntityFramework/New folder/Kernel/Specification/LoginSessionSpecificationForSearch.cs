using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Modules.SecurityManagement;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class LoginSessionSpecificationForSearch : ISpecification<LoginSession>
    {
        private LoginSessionSearchCriteria _criteria;

        public LoginSessionSpecificationForSearch(LoginSessionSearchCriteria criteria)
        {
            _criteria = criteria;
        }
        public Expression<Func<LoginSession, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<LoginSession>();

                if (_criteria.LoginId != Guid.Empty)
                    builder = builder.And(x => x.Login.Id.Equals(_criteria.LoginId)).And(x => x.EndTime == null);

                return builder;
            }
        }
    }
}

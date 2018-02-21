using Modules.SecurityManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using System.Linq.Expressions;
using LinqKit;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class LoginSpecificationForSearch : ISpecification<Login>
    {
        private LoginSearchCriteria _criteria;

        public LoginSpecificationForSearch(LoginSearchCriteria criteria)
        {
            _criteria = criteria;
        }
        public Expression<Func<Login, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Login>();

                if (!string.IsNullOrEmpty(_criteria.UserName) && !string.IsNullOrEmpty(_criteria.Password) && _criteria.RoleId != Guid.Empty)
                    builder = builder.And(x => x.Password.ToString().Equals(_criteria.Password)).And(x => x.UserName.ToString().Equals(_criteria.UserName)).And(x => x.Role.Id.Equals(_criteria.RoleId));

                if (!string.IsNullOrEmpty(_criteria.UserName) && !string.IsNullOrEmpty(_criteria.Password) && _criteria.RoleId == Guid.Empty)
                    builder = builder.And(x => x.Password.ToUpper().Equals(_criteria.Password.ToUpper())).And(x => x.UserName.ToUpper().Equals(_criteria.UserName.ToUpper()));

                if (string.IsNullOrEmpty(_criteria.UserName) && string.IsNullOrEmpty(_criteria.Password) && _criteria.RoleId != Guid.Empty)
                    builder = builder.And(x => x.Role.Id.Equals(_criteria.RoleId));

                if (string.IsNullOrEmpty(_criteria.UserName) && string.IsNullOrEmpty(_criteria.Password) && _criteria.RoleId == Guid.Empty && !string.IsNullOrEmpty(_criteria.Role))
                    builder = builder.And(x => x.Role.RoleName.ToUpper().Equals(_criteria.Role.ToUpper()));

                if (!string.IsNullOrEmpty(_criteria.UserName) && string.IsNullOrEmpty(_criteria.Password) && _criteria.RoleId == Guid.Empty)
                    builder = builder.And(x => x.UserName.ToUpper().Equals(_criteria.UserName.ToUpper()));

                return builder;
            }
        }
    }
}

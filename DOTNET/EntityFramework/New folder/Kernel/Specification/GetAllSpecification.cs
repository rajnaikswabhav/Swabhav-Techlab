using System;
using System.Linq.Expressions;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Framework.Repository;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class GetAllSpecification<T> : ISpecification<T> where T : AggregateEntity
    {
        public Expression<Func<T, bool>> Expression
        {
            get
            {
                return x => true;
            }
        }
    }
}

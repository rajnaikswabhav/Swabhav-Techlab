using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Framework.Repository
{
    public interface ISpecification<T> where T : AggregateEntity
    {
        Expression<Func<T, bool>> Expression { get; }
    }
}

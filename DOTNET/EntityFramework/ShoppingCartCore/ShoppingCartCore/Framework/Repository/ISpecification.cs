using System;
using System.Linq.Expressions;

namespace ShoppingCartCore.Framework.Repository
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>> Expression  { get; }
    }
}
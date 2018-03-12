using ShoppingCartCore.Framework.Model;
using System;
using System.Linq.Expressions;

namespace ShoppingCartCore.Framework.Repository
{
    public interface ISpecification<T> where T : Entity
    {
        Expression<Func<T, bool>> Expression  { get; }
    }
}
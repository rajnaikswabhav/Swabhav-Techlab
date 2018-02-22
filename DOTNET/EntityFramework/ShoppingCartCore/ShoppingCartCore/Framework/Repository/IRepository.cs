using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Repository
{
    interface IRepository<T> where T : class
    {
        void Add(T entity);
        T GetById(int id);
        IList<T> Find(ISpecification<T> specification);
    }
}

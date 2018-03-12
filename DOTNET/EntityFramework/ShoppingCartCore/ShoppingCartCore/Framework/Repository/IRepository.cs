using ShoppingCartCore.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Repository
{
    interface IRepository<T> where T : Entity
    {
        void Add(T entity);
        void Delete(Guid entityId);
        void Update(T entity);

        T GetById(Guid id);
        IList<T> Find(ISpecification<T> specification);
        IList<T> Get();
        
    }
}

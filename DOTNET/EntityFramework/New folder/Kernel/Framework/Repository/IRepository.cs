using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Framework.Repository
{
    public interface IRepository<T> where T: AggregateEntity
    {
        Guid Add(T entity);
        void Update(T entity);
        void Delete(Guid entityId);

        T GetById(Guid entityId);
        IList<T> Find(ISpecification<T> specification);
        int Count(ISpecification<T> specifivation);
    }
}

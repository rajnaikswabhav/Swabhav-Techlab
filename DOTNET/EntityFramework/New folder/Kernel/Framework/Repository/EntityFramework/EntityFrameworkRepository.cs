using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using LinqKit;

namespace Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : AggregateEntity
    {
        public Guid Add(T entity)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                unitOfWork.DbContext.Set<T>().Add(entity);
                //  unitOfWork.DbContext.SaveChanges();
                unitOfWork.SaveChanges();
                return entity.Id;
            }
        }

        public virtual void Update(T entity)
        {
            //Nothing to do here. Entity framework will automatically save changed entities
        }

        public void Delete(Guid entityId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Writing))
            {
                unitOfWork.DbContext.Set<T>().Remove(GetById(entityId));
                //  unitOfWork.DbContext.SaveChanges();

                unitOfWork.SaveChanges();
            }
        }

        public T GetById(Guid entityId)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<T>().SingleOrDefault(x => x.Id == entityId);
            }
        }

        public IList<T> Find(ISpecification<T> specification)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<T>().AsExpandable().Where(specification.Expression);
                return queryable.ToList();
            }
        }

        public IList<T> Get()
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<T>().ToList();
            }
        }

        public int Count(ISpecification<T> specifivation)
        {
            using (var unitOfWork = new UnitOfWorkScope<EuphoriaContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<T>().AsExpandable().Where(specifivation.Expression).Count();
            }
        }
    }
}

using LinqKit;
using ShoppingCartCore.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Repository.EntityFramework
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : Entity
    {
        public void Add(T entity)
        {
            using (var unitOfWork = new UnitOfWorkScope<ShoppingCartDbContext>(UnitOfWorkScopePurpose.Writing))
            {
                unitOfWork.DbContext.Set<T>().Add(entity);
                unitOfWork.SaveChanges();
            }
        }

        public void Delete(Guid entityId)
        {
            using (var unitOfWork = new UnitOfWorkScope<ShoppingCartDbContext>(UnitOfWorkScopePurpose.Writing))
            {
                unitOfWork.DbContext.Set<T>().Remove(GetById(entityId));
                unitOfWork.SaveChanges();
            }
        }

        public IList<T> Find(ISpecification<T> specification)
        {
            using (var unitOfWork = new UnitOfWorkScope<ShoppingCartDbContext>(UnitOfWorkScopePurpose.Reading))
            {
                var queryable = unitOfWork.DbContext.Set<T>().AsExpandable().Where(specification.Expression);
                return queryable.ToList();
            }
        }

        public IList<T> Get()
        {
            using (var unitOfWork = new UnitOfWorkScope<ShoppingCartDbContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<T>().ToList();
            }
        }

        public T GetById(Guid id)
        {
            using (var unitOfWork = new UnitOfWorkScope<ShoppingCartDbContext>(UnitOfWorkScopePurpose.Reading))
            {
                return unitOfWork.DbContext.Set<T>().SingleOrDefault(x => x.Id == id);
            }
        }

        public void Update(T enity)
        {
            using (var unitOfWork = new UnitOfWorkScope<ShoppingCartDbContext>(UnitOfWorkScopePurpose.Writing))
            {
                var user = GetById(enity.Id);
                unitOfWork.DbContext.Set<T>().Remove(user);
                unitOfWork.DbContext.Set<T>().Add(enity);
                unitOfWork.SaveChanges();
            }
        }
    }
}

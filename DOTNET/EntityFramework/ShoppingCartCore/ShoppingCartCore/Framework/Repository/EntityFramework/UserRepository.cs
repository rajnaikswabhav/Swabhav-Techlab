using ShoppingCartCore.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Repository.EntityFramework
{
    class UserRepository : IRepository<User>
    {
        private ShoppingCartDbContext userContext = new ShoppingCartDbContext();

        public void Add(User user)
        {
            userContext.Users.Add(user);
            userContext.SaveChanges();
        }

        public IList<User> Find(ISpecification<User> specification)
        {
            return userContext.Users.Where(specification.Expression).ToList();
        }

        public User GetById(int id)
        {
            return userContext.Users.Single(u => u.UserId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Repository.EntityFramework
{
    class CustomerRepository : IRepository<Customer>
    {
        private ShoppingCartDbContext customerContext = new ShoppingCartDbContext();

        public void Add(Customer customer)
        {
            customerContext.Customers.Add(customer);
            customerContext.SaveChanges();
        }

        public IList<Customer> Find(ISpecification<Customer> specification)
        {
            return customerContext.Customers.Where(specification.Expression).ToList();
        }

        public Customer GetById(int id)
        {
            return customerContext.Customers.Single(c => c.Id == id);
        }

        public IList<Customer> Get()
        {
            var customerList = customerContext.Customers.Select(c => c).ToList();
            return customerList;
        }
    }
}

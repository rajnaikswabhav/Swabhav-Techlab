using ShoppingCartCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Repository.EntityFramework
{
    class ProductRepository : IRepository<Product>
    {
        private ShoppingCartDbContext productContext = new ShoppingCartDbContext();
        
        public void Add(Product product)
        {
            productContext.Products.Add(product);
            productContext.SaveChanges();
        }

        public IList<Product> Find(ISpecification<Product> specification)
        {
            return productContext.Products.Where(specification.Expression).ToList();
        }

        public Product GetById(int id)
        {
            return productContext.Products.Single(p => p.ProductId == id);
        }

        public IList<Product> Get()
        {
            var productList = productContext.Products.Select(p => p).ToList();
            return productList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Models
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAll();
        public Product GetById(Guid id);
        public Product Create(Product product);
        public bool Delete(Guid id);

    }
}

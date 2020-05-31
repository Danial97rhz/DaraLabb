using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(Guid id);
    }
}

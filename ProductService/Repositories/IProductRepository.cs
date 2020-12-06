using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaraLabb.Web.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(Guid id);
    }
}

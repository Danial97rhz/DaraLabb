using System.Collections.Generic;

namespace ProductsService.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
    }
}

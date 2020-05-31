using ProductService.Models;
using System;
using System.Collections.Generic;

namespace DaraLabb.Web.Models
{
    class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> GetAll => new List<Category>
        { 
            new Category { Id = Guid.NewGuid(), Name = "Rings" },
            new Category { Id = Guid.NewGuid(), Name = "Earrings" }
        };
    }
}

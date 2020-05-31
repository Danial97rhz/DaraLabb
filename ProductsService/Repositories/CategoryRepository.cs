using ProductsService.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductsService.Models
{
    class CategoryRepository : ICategoryRepository
    {
        private readonly ProductDbContext _context;

        public CategoryRepository(ProductDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = _context.Categories.ToList();

            return categories;
        }
    }
}

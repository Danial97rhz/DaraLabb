using ProductsService.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductsService.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public Product Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            return product;
        }

        public bool Delete(Guid id)
        {
            try
            {
                var product = GetById(id);
                _context.Products.Remove(product);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            var products = _context.Products.ToList();

            return products;
        }

        public Product GetById(Guid id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            return product;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext (DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

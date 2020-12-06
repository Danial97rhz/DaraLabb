
using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data
{
    public static class DbInitializer
    {
        public static void Initialize(OrderDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}

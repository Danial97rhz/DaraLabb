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
            var ps = new List<Product>();
            Guid newGuid1 = Guid.NewGuid();
            Guid newGuid2 = Guid.NewGuid();
            if (context.Products.Any())
            {
                return;
            }

            var products = new Product[]
            {
                new Product {},
                new Product {},
                new Product {},
                new Product {},
                new Product {},
                new Product {}
            };

            foreach (var p in products)
            {
                context.Products.Add(p);
                ps.Add(p);
            }


            var orders = new List<Order>
            {
                new Order{ Date=DateTime.Now, Id = newGuid1},
                new Order{ Date=DateTime.Now, Id = newGuid2}
            };

            foreach (var o in orders)
            {
                context.Orders.Add(o);
            }

            var orderRows = new List<OrderRow>
            {
                new OrderRow{Product = ps[0], Quantity=2, orderId = newGuid1},
                new OrderRow{Product = ps[3], Quantity=3, orderId = newGuid1},
                new OrderRow{Product = ps[1], Quantity=1, orderId = newGuid2},
                new OrderRow{Product = ps[2], Quantity=1, orderId = newGuid2},
                new OrderRow{Product = ps[3], Quantity=1, orderId = newGuid2}
            };

            foreach (var or in orderRows)
            {
                context.OrderRows.Add(or);
            }

            context.SaveChanges();
        }
    }
}

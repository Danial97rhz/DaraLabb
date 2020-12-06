using OrderService.Data;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public Order Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _context.Orders.ToList();

            return orders;
        }

        public Order GetById(Guid id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);

            return order;
        }
    }
}

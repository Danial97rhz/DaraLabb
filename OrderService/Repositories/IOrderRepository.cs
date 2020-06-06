using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> GetAll();
        public Order GetById(Guid id);
        public Order Create(Order order);
    }
}

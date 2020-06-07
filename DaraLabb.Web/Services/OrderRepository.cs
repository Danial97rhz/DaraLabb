using DaraLabb.Web.Models;
using System.Collections.Generic;

namespace DaraLabb.Web.Services
{
    public class OrderRepository : IOrderRepository
    {
        List<Order> orders = new List<Order>();

        public IEnumerable<Order> GetAll()
        {
            return orders;
        }

        public Order addToOrderHirtory(Order order)
        {
            orders.Add(order);
            return order;
            
        }

    }
}

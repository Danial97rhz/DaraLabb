using DaraLabb.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaraLabb.Web.Services
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> GetAll();
        Order addToOrderHirtory(Order order);
    }
}

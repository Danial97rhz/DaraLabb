using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public List<OrderRow> OrderRows { get; set; }
    }
}

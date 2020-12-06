using System;

namespace OrderService.Models
{
    public class OrderRow
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}

using DaraLabb.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaraLabb.Web.Models
{
    public class Order
    {
        public Order()
        {
            OrderRows = new List<OrderRow>();
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderRow> OrderRows { get; set; }
        public int ItemCount { get; set; }
        public string Address { get; set; }
    }
    public class OrderRow
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public static explicit operator OrderRow(CartItem cartItem)
        {
            var orderRow = new OrderRow()
            {
                Product = cartItem.Product,
                Quantity = cartItem.Quantity
            };

            return orderRow;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public ActionResult<Order> GetAll()
        {
            var orders = _orderRepository.GetAll();
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult<Order> GetById(Guid id)
        {
            var order = _orderRepository.GetById(id);
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> Create([FromBody] Order order)
        {
            var createdOrder = _orderRepository.Create(order);
            return Ok(createdOrder);
        }
    }
}
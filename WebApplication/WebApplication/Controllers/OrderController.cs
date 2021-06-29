using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.models;

namespace WebApplication.controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet("{id:long}")]
        public ActionResult<string> Home([FromRoute(Name = "id")] long id)
        {
            OrderRepository orderRepository = new OrderRepository();
            List<Order> data = orderRepository.SelectById(id);
            string json = orderRepository.ToJson(data);
            return Ok(json);
        }

        [HttpPost("create")]
        public ActionResult<string> Home([FromBody] OrderSet order)
        {
            OrderRepository orderRepository = new OrderRepository();
            long id = orderRepository.Insert(order);
            return Ok(id);
        }
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.models;

namespace WebApplication.controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //public ActionResult<string> Home([FromQuery(Name = "id")] long id)
        [HttpGet("{id:long}")]
        public ActionResult<string> Home([FromRoute(Name = "id")] long id)
        {
            OrderRepository orderRepository = new OrderRepository();
            List<Order> data = orderRepository.SelectById(id);
            string json = orderRepository.ToJson(data);
            return Ok(json);
        }

        [HttpPost("")]
        public ActionResult<string> Home([FromBody] Order order)
        {
            OrderRepository orderRepository = new OrderRepository();
            bool res = orderRepository.Insert(order);
            if (res)
                return Ok("OK");
            return Problem("WRONG DATA PARAM");
        }
    }
}
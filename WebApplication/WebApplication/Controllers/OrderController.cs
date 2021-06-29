using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.models;

namespace WebApplication.controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrderController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id:long}")]
        public ActionResult<string> Home([FromRoute(Name = "id")] long id)
        {
            List<Order> data = _repository.SelectById(id);
            string json = _repository.ToJson(data);
            return Ok(json);
        }

        [HttpPost("create")]
        public ActionResult<string> Home([FromBody] OrderSet order)
        {
            long id = _repository.Insert(order);
            return Ok(id);
        }
    }
}
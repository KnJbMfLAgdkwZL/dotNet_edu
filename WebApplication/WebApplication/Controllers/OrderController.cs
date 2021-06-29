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
        public ActionResult<Order> Home([FromRoute(Name = "id")] long id)
        {
            var data = _repository.SelectById(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpPost("create")]
        public ActionResult<long> Home([FromBody] OrderSet order)
        {
            return Ok(_repository.Insert(order));
        }
    }
}
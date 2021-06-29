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
        public Order Home([FromRoute(Name = "id")] long id)
        {
            return _repository.SelectById(id);
        }

        [HttpPost("create")]
        public long Home([FromBody] OrderSet order)
        {
            return _repository.Insert(order);
        }
    }
}
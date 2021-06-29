using System.Threading.Tasks;
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
        public async Task<ActionResult<Order>> GetAsync([FromRoute(Name = "id")] long id)
        {
            var data = await _repository.SelectByIdAsync(id);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpPost("create")]
        public async Task<ActionResult<long>> CreateAsync([FromBody] OrderSet order)
        {
            return Ok(await _repository.InsertAsync(order));
        }
    }
}
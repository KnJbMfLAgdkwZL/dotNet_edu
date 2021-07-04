using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Configs;
using WebApplication.Exceptions;
using WebApplication.models;

namespace WebApplication.controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly ClientsBlacklistConfig _clientsBlacklistConfig;

        public OrderController(IOrderRepository repository, IOptions<ClientsBlacklistConfig> opt)
        {
            _repository = repository;
            _clientsBlacklistConfig = opt.Value;
        }

        [ProducesResponseType(200, Type = typeof(Order))]
        [ProducesResponseType(404)]
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Order>> GetAsync([FromRoute(Name = "id")] long id, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var data = await _repository.SelectByIdAsync(id, token);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [ProducesResponseType(200, Type = typeof(long))]
        [ProducesResponseType(404)]
        [HttpPost("create")]
        public async Task<ActionResult<long>> CreateAsync([FromBody] OrderSet order, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            var res = _clientsBlacklistConfig.Clients.Contains(order.ClientId);
            if (res)
            {
                throw new BusinessException();
            }

            return Ok(await _repository.InsertAsync(order, token));
        }
    }
}
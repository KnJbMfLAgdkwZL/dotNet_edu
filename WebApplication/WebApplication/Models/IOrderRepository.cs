using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.models
{
    public interface IOrderRepository
    {
        public Task<Order> SelectByIdAsync(long id, CancellationToken token);
        public Task<long> InsertAsync(OrderSet order, CancellationToken token);
    }
}
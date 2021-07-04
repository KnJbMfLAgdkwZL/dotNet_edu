using System.Threading;
using System.Threading.Tasks;
using WebApplication.models;

namespace WebApplication.Repositories
{
    public interface IOrderRepository
    {
        public Task<Order> SelectByIdAsync(long id, CancellationToken token);
        public Task<long> InsertAsync(OrderSet order, CancellationToken token);
    }
}
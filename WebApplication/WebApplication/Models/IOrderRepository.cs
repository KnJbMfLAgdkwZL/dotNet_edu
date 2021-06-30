using System.Threading.Tasks;

namespace WebApplication.models
{
    public interface IOrderRepository
    {
        public Task<Order> SelectByIdAsync(long id);
        public Task<long> InsertAsync(OrderSet order);
    }
}
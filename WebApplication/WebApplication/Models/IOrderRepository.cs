using System.Threading.Tasks;

namespace WebApplication.models
{
    public interface IOrderRepository
    {
        public Task<Order> SelectById(long id);
        public Task<long> Insert(OrderSet order);
    }
}
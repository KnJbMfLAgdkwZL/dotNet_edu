using System.Collections.Generic;

namespace WebApplication.models
{
    public interface IOrderRepository
    {
        public List<Order> SelectById(long id);
        public long Insert(Order order);
    }
}
using System.Collections.Generic;

namespace WebApplication.models
{
    public interface IOrderRepository
    {
        public Order SelectById(long id);
        public long Insert(OrderSet order);
    }
}
using System.Collections.Generic;

namespace WebApplication.models
{
    public interface IOrderRepository
    {
        public List<Order> SelectById(long id);
        public long Insert(OrderSet order);
        public string ToJson(List<Order> data);
    }
}
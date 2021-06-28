using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace WebApplication.models
{
    public class OrderRepository : Repository
    {
        protected override string Table { set; get; } = "order";

        public List<Order> SelectById(long id)
        {
            List<Dictionary<string, object>> rows = Select(new Dictionary<string, string>()
            {
                {"id", id.ToString()}
            });
            return rows.Select(i => new Order(i["id"], i["name"], i["description"], i["dateCreate"])).ToList();
        }

        public bool Insert(Order order)
        {
            return base.Insert(new Dictionary<string, string>()
            {
                {"name", order.name},
                {"description", order.description},
                {"dateCreate", order.dateCreate}
            });
        }

        public string ToJson(List<Order> data)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(data, options);
        }
    }
}
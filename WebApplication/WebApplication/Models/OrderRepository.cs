using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.models
{
    public class OrderRepository : Repository, IOrderRepository
    {
        protected override string Table { set; get; } = "order";

        public async Task<Order> SelectByIdAsync(long id)
        {
            var rows = await SelectAsync(new Dictionary<string, string>()
            {
                {"id", id.ToString()}
            });
            return rows.Select(i => new Order(
                    long.Parse(i["id"].ToString() ?? string.Empty),
                    i["name"].ToString(),
                    i["description"].ToString(),
                    DateTime.Parse(i["dateCreate"].ToString() ?? string.Empty)
                )
            ).ToList().FirstOrDefault();
        }

        public async Task<long> InsertAsync(OrderSet order)
        {
            Connection.Open();
            var command = Connection.CreateCommand();
            command.CommandText =
                $"INSERT INTO '{Table}' (id, name, description, dateCreate) VALUES (null, $name, $description, datetime('now'));" +
                $"select last_insert_rowid()";
            command.Parameters.AddWithValue($"$name", order.name);
            command.Parameters.AddWithValue($"$description", order.description);
            var temp = command.ExecuteScalar();
            var id = long.Parse(temp.ToString());
            Connection.Close();
            return id;
        }
    }
}
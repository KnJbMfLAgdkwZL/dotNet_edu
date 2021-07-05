using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.models;

namespace WebApplication.Repositories
{
    public class OrderRepositoryV1 : Repository, IOrderRepository
    {
        protected override string Table { get; set; } = "order";

        public async Task<Order> SelectByIdAsync(long id, CancellationToken token)
        {
            var rows = await SelectAsync(token, new Dictionary<string, string>()
            {
                {"id", id.ToString()}
            });
            return rows.Select(i => new Order(
                    long.Parse(i["id"].ToString() ?? string.Empty),
                    i["name"].ToString(),
                    i["description"].ToString(),
                    DateTime.Parse(i["dateCreate"].ToString() ?? string.Empty)
                )
            ).FirstOrDefault();
        }

        public async Task<long> InsertAsync(OrderSet order, CancellationToken token)
        {
            Connection.Open();
            var command = Connection.CreateCommand();
            command.CommandText =
                $"INSERT INTO '{Table}' (id, name, description, dateCreate, clientId) VALUES (null, $name, $description, datetime('now'), $clientId);" +
                $"select last_insert_rowid()";
            command.Parameters.AddWithValue($"$name", order.Name);
            command.Parameters.AddWithValue($"$description", order.Description);
            command.Parameters.AddWithValue($"$clientId", order.ClientId);
            var temp = await command.ExecuteScalarAsync(token);
            var id = long.Parse(temp?.ToString() ?? string.Empty);
            Connection.Close();
            return id;
        }
    }
}
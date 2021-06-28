using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.Data.Sqlite;

namespace WebApplication.models
{
    public class OrderRepository : Repository, IOrderRepository
    {
        protected override string Table { set; get; } = "order";

        public List<Order> SelectById(long id)
        {
            List<Dictionary<string, object>> rows = Select(new Dictionary<string, string>()
            {
                {"id", id.ToString()}
            });


            return rows.Select(i => new Order(
                    long.Parse(i["id"].ToString() ?? string.Empty),
                    i["name"].ToString(),
                    i["description"].ToString(),
                    DateTime.Parse(i["dateCreate"].ToString() ?? string.Empty)
                )
            ).ToList();
        }

        public bool Insert(Order order)
        {
            Connection.Open();
            SqliteCommand command = Connection.CreateCommand();
            command.CommandText =
                $"INSERT INTO '{Table}' (id, name, description, dateCreate) VALUES (null, $name, $description, datetime('now'))";
            command.Parameters.AddWithValue($"$name", order.name);
            command.Parameters.AddWithValue($"$description", order.description);
            return command.ExecuteNonQuery() > 0;
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
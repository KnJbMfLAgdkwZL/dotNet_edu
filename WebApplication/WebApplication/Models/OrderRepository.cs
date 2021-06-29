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

        public long Insert(OrderSet order)
        {
            Connection.Open();
            SqliteCommand command = Connection.CreateCommand();
            command.CommandText =
                $"INSERT INTO '{Table}' (id, name, description, dateCreate) VALUES (null, $name, $description, datetime('now'));" +
                $"select last_insert_rowid()";
            command.Parameters.AddWithValue($"$name", order.name);
            command.Parameters.AddWithValue($"$description", order.description);
            Object temp = command.ExecuteScalar();
            long id = long.Parse(temp.ToString());
            Connection.Close();
            return id;
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
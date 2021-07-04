using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication.models
{
    public abstract class Repository
    {
        protected abstract string Table { get; set; }
        private string Path { get; set; } = "../mainDB.sqlite";
        protected SqliteConnection Connection { get; set; }

        protected Repository()
        {
            Connection = new SqliteConnection($"Data Source={Path}");
        }

        protected async Task<List<Dictionary<string, object>>> SelectAsync(CancellationToken token,
            Dictionary<string, string> arguments = null)
        {
            token.ThrowIfCancellationRequested();
            Connection.Open();
            await using var command = Connection.CreateCommand();
            var sql = $"SELECT * FROM '{Table}'";
            if (arguments != null)
            {
                foreach (var (key, val) in arguments)
                    command.Parameters.AddWithValue($"${key}", val);
                sql += " WHERE " + string.Join(" AND ", arguments.Select(i => $"{i.Key} = ${i.Key}").ToArray());
            }

            command.CommandText = sql;
            await using var reader = await command.ExecuteReaderAsync(token);
            var data = new List<Dictionary<string, object>>();
            while (await reader.ReadAsync(token))
                data.Add(Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue));
            Connection.Close();
            return data;
        }
    }
}
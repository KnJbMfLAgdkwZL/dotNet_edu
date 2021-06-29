using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace WebApplication.models
{
    public abstract class Repository
    {
        protected abstract string Table { set; get; }
        private string Path { set; get; } = "../mainDB.sqlite";
        protected SqliteConnection Connection { set; get; }

        protected Repository()
        {
            Connection = new SqliteConnection($"Data Source={Path}");
        }

        protected async Task<List<Dictionary<string, object>>> Select(Dictionary<string, string> arguments = null)
        {
            Connection.Open();
            var command = Connection.CreateCommand();
            var sql = $"SELECT * FROM '{Table}'";
            if (arguments != null)
            {
                foreach (var (key, val) in arguments)
                    command.Parameters.AddWithValue($"${key}", val);
                sql += " WHERE " + string.Join(" AND ", arguments.Select(i => $"{i.Key} = ${i.Key}").ToArray());
            }

            command.CommandText = sql;
            var reader = command.ExecuteReader();
            var data = new List<Dictionary<string, object>>();
            while (reader.Read())
                data.Add(Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue));
            Connection.Close();
            return data;
        }

        protected async Task<long> Insert(Dictionary<string, string> arguments)
        {
            Connection.Open();
            var command = Connection.CreateCommand();
            var keys = string.Join(", ", arguments.Select(i => $"{i.Key}").ToArray());
            var values = string.Join(", ", arguments.Select(i => $"${i.Key}").ToArray());
            var sql = $"INSERT INTO '{Table}' ({keys}) VALUES ({values});" +
                      $"select last_insert_rowid()";
            foreach (var (key, val) in arguments)
                command.Parameters.AddWithValue($"${key}", val);
            command.CommandText = sql;
            var temp = command.ExecuteScalar();
            long id = int.Parse(temp.ToString());
            Connection.Close();
            return id;
        }
    }
}
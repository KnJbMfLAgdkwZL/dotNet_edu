using System.Collections.Generic;
using System.Linq;
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

        protected List<Dictionary<string, object>> Select(Dictionary<string, string> arguments = null)
        {
            Connection.Open();
            SqliteCommand command = Connection.CreateCommand();
            string sql = $"SELECT * FROM '{Table}'";
            if (arguments != null)
            {
                foreach (var (key, val) in arguments)
                    command.Parameters.AddWithValue($"${key}", val);
                sql += " WHERE " + string.Join(" AND ", arguments.Select(i => $"{i.Key} = ${i.Key}").ToArray());
            }

            command.CommandText = sql;
            SqliteDataReader reader = command.ExecuteReader();
            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            while (reader.Read())
                data.Add(Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue));
            Connection.Close();
            return data;
        }

        protected bool Insert(Dictionary<string, string> arguments)
        {
            Connection.Open();
            SqliteCommand command = Connection.CreateCommand();
            string keys = string.Join(", ", arguments.Select(i => $"{i.Key}").ToArray());
            string values = string.Join(", ", arguments.Select(i => $"${i.Key}").ToArray());
            string sql = $"INSERT INTO '{Table}' ({keys}) VALUES ({values})";
            foreach (var (key, val) in arguments)
                command.Parameters.AddWithValue($"${key}", val);
            command.CommandText = sql;
            return command.ExecuteNonQuery() > 0;
        }
    }
}
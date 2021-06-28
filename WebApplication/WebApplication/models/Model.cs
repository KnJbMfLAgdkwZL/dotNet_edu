using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace WebApplication.models
{
    public abstract class Model
    {
        private string Path { set; get; } = "../mainDB.sqlite";
        private SqliteConnection Connection { set; get; }

        protected Model()
        {
            Connection = new SqliteConnection($"Data Source={Path}");
        }

        /// <summary>
        /// Выборка данных из таблицы, эквивалент sql запросу:
        /// "SELECT * FROM table"
        /// </summary>
        /// <param name="arguments">Не обяз. Условие поиска [ключь=значение, ключь=значение]</param>
        /// <returns>Данные выборки</returns>
        protected List<Dictionary<string, object>> Select(Dictionary<string, string> arguments = null)
        {
            Connection.Open();
            var command = Connection.CreateCommand();
            var table = GetType().Name;
            var sql = $"SELECT * FROM '{table}'";
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

        /// <summary>
        /// Вставка данных в таблицу, эквивалент sql запросу:
        /// INSERT INTO table (name, desc) VALUES('Имя', 'Описание')
        /// </summary>
        /// <param name="arguments">Обяз. Аргументы, данные для вставки [ключь=значение, ключь=значение]</param>
        /// <returns>Вернет true в случае успеха</returns>
        protected bool Insert(Dictionary<string, string> arguments)
        {
            Connection.Open();
            var command = Connection.CreateCommand();
            var table = GetType().Name;
            var keys = string.Join(", ", arguments.Select(i => $"{i.Key}").ToArray());
            var values = string.Join(", ", arguments.Select(i => $"${i.Key}").ToArray());
            var sql = $"INSERT INTO '{table}' ({keys}) VALUES ({values})";
            foreach (var (key, val) in arguments)
                command.Parameters.AddWithValue($"${key}", val);
            command.CommandText = sql;
            return command.ExecuteNonQuery() > 0;
        }
    }
}
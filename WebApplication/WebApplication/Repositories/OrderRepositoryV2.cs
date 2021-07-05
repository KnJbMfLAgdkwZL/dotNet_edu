using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using WebApplication.Configs;
using WebApplication.models;

namespace WebApplication.Repositories
{
    public class OrderRepositoryV2 : IOrderRepository
    {
        private string ConnectionStr { get; set; }

        public OrderRepositoryV2(IOptions<DataBaseConfig> opt)
        {
            ConnectionStr = opt.Value.ConnectionStrings;
        }

        public async Task<Order> SelectByIdAsync(long id, CancellationToken token)
        {
            const string sql = "SELECT * FROM 'order' WHERE id = :Id ;";
            var command = new CommandDefinition(sql, new {Id = id}, cancellationToken: token);
            await using var con = new SqliteConnection(ConnectionStr);
            var result = await con.QueryAsync<Order>(command);
            return result.FirstOrDefault();
        }

        public async Task<long> InsertAsync(OrderSet order, CancellationToken token)
        {
            var param = new
            {
                name = order.Name,
                description = order.Description,
                clientId = order.ClientId
            };
            const string sql =
                "INSERT INTO 'order' (id, name, description, dateCreate, clientId)" +
                "VALUES (null, :name, :description, datetime('now'), :clientId) ;" +
                "select last_insert_rowid() ;";
            var command = new CommandDefinition(sql, param, cancellationToken: token);
            await using var con = new SqliteConnection(ConnectionStr);
            return await con.ExecuteScalarAsync<long>(command);
        }
    }
}
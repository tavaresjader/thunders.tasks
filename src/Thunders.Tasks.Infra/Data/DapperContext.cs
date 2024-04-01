using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using Thunders.Tasks.Core.Data;

namespace Thunders.Tasks.Infra.Data
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;

        private IDbConnection _dbConnection;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;

            CreateConnection();
        }

        public bool CreateConnection()
        {
            var connectionString = _configuration.GetValue<string>("Database:ConnectionString") ?? throw new ApplicationException("Connection String Not Found");

            _dbConnection = new MySqlConnection(connectionString);

            return true;
        }

        public IDbTransaction CreateTransaction()
        {
            return _dbConnection.BeginTransaction();
        }

        public async Task<bool> ExecuteAsync(IDbConnection connection, CommandDefinition commandDefinition)
        {
            await connection.ExecuteAsync(commandDefinition);
            return true;
        }

        public IDbConnection GetConnection()
        {
            return _dbConnection;
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(IDbConnection connection, CommandDefinition commandDefinition)
        {
            return await connection.QueryFirstOrDefaultAsync<T>(commandDefinition);
        }

        public async Task<IEnumerable<T?>> QueryAsync<T>(IDbConnection connection, CommandDefinition commandDefinition)
        {
            return await connection.QueryAsync<T>(commandDefinition);
        }
    }
}

using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using Thunders.Tasks.Core.Data;

namespace Thunders.Tasks.Infra.Data
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("Database:ConnectionString") ?? string.Empty;
        }

        public IDbConnection CreateConnection()
            => new MySqlConnection(_connectionString);

    }
}

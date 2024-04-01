using Dapper;
using System.Data;

namespace Thunders.Tasks.Core.Data
{
    public interface IDapperContext
    {
        public bool CreateConnection();
        public IDbConnection GetConnection();
        public IDbTransaction CreateTransaction();

        public Task<bool> ExecuteAsync(IDbConnection connection, CommandDefinition commandDefinition);

        public Task<T?> QueryFirstOrDefaultAsync<T>(IDbConnection connection, CommandDefinition commandDefinition);
        public Task<IEnumerable<T>?> QueryAsync<T>(IDbConnection connection, CommandDefinition commandDefinition);
    }
}

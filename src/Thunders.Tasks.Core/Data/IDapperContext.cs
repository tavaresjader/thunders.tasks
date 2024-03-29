using System.Data;

namespace Thunders.Tasks.Core.Data
{
    public interface IDapperContext
    {
        public IDbConnection CreateConnection();
    }
}

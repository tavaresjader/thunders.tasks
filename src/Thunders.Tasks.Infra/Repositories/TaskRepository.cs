using Dapper;
using ErrorOr;
using System.Data;
using Thunders.Tasks.Core.Data;
using Thunders.Tasks.Core.Dtos;
using Thunders.Tasks.Core.Repositories;

namespace Thunders.Tasks.Infra.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDapperContext _dapperContext;

        public TaskRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<ErrorOr<int>> CreateAsync(TaskDto dto, CancellationToken ct)
        {
            int taskId = 0;

            using IDbConnection dbConnection = _dapperContext.CreateConnection();

            IDbTransaction? transaction = null;

            try
            {
                dbConnection.Open();

                transaction = dbConnection.BeginTransaction();

                var querySql = @"insert into Task 
                                    (Title, Description, EstimateStartDate, EstimateEndDate, Deleted, CreatedAt, UpdatedAt) 
                                values (@title, @description, @estimateStartDate, @estimateEndDate, @deleted, @createdAt, @updatedAt)";

                await dbConnection.ExecuteAsync(new CommandDefinition(querySql, dto, transaction, cancellationToken: ct));

                taskId = await dbConnection.QueryFirstOrDefaultAsync<int>(new CommandDefinition("select max(Id) from Task", null, transaction, cancellationToken: ct));

                transaction.Commit();
            }
            catch (Exception)
            {
                if (transaction is not null)
                    transaction.Rollback();

                throw;
            }
            finally 
            {
                if (dbConnection.State is ConnectionState.Open)
                    dbConnection.Close();
            }
                  
            return taskId;
        }

        public async Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken ct)
        {
            using IDbConnection dbConnection = _dapperContext.CreateConnection();

            IDbTransaction? transaction = null;

            try
            {
                dbConnection.Open();

                transaction = dbConnection.BeginTransaction();

                var querySql = @"update Task set Deleted = 1, UpdatedAt = @updatedAt where Id = @id";

                await dbConnection.ExecuteAsync(new CommandDefinition(querySql, new { id, updatedAt = DateTime.Now }, transaction, cancellationToken: ct));

                transaction.Commit();

                return true;
            }
            catch (Exception)
            {
                if (transaction is not null)
                    transaction.Rollback();

                throw;
            }
            finally
            {
                if (dbConnection.State is ConnectionState.Open)
                    dbConnection.Close();
            }
        }

        public async Task<ErrorOr<IList<TaskDto>>> GetAllAsync(CancellationToken ct)
        {
            using IDbConnection dbConnection = _dapperContext.CreateConnection();

            try
            {
                dbConnection.Open();

                var querySql = @"select * from Task where Deleted = 0 order by EstimateStartDate asc";

                var results = await dbConnection.QueryAsync<TaskDto>(new CommandDefinition(querySql, cancellationToken: ct));

                return results.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dbConnection.State is ConnectionState.Open)
                    dbConnection.Close();
            }
        }

        public async Task<ErrorOr<TaskDto>> GetByIdAsync(int id, CancellationToken ct)
        {
            using IDbConnection dbConnection = _dapperContext.CreateConnection();

            try
            {
                dbConnection.Open();

                var querySql = @"select * from Task where Deleted = 0 and Id = @id";

                var result = await dbConnection.QueryFirstOrDefaultAsync<TaskDto>(new CommandDefinition(querySql, new { id }, cancellationToken: ct));

                if (result is null)
                    return Error.NotFound();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dbConnection.State is ConnectionState.Open)
                    dbConnection.Close();
            }
        }

        public async Task<ErrorOr<bool>> UpdateAsync(TaskDto dto, CancellationToken ct)
        {
            using IDbConnection dbConnection = _dapperContext.CreateConnection();

            IDbTransaction? transaction = null;

            try
            {
                dbConnection.Open();

                transaction = dbConnection.BeginTransaction();

                var querySql = @"update Task set Title = @title, Description = @description, EstimateStartDate = @estimateStartDate, EstimateEndDate = @estimateEndDate, UpdatedAt = @updatedAt where Id = @id";

                await dbConnection.ExecuteAsync(new CommandDefinition(querySql, dto, transaction, cancellationToken: ct));

                transaction.Commit();

                return true;
            }
            catch (Exception)
            {
                if (transaction is not null)
                    transaction.Rollback();

                throw;
            }
            finally
            {
                if (dbConnection.State is ConnectionState.Open)
                    dbConnection.Close();
            }
        }
    }
}


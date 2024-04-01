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

            IDbConnection dbConnection = _dapperContext.GetConnection();

            IDbTransaction? transaction = null;

            try
            {
                dbConnection.Open();

                transaction = _dapperContext.CreateTransaction();

                var querySql = @"insert into Task 
                                    (Title, Description, EstimateStartDate, EstimateEndDate, Deleted, CreatedAt, UpdatedAt) 
                                values (@title, @description, @estimateStartDate, @estimateEndDate, @deleted, @createdAt, @updatedAt)";

                await _dapperContext.ExecuteAsync(dbConnection, new CommandDefinition(querySql, dto, transaction, cancellationToken: ct));

                taskId = await _dapperContext.QueryFirstOrDefaultAsync<int>(dbConnection, new CommandDefinition("select max(Id) from Task", null, transaction, cancellationToken: ct));

                transaction.Commit();
            }
            catch (Exception ex)
            {
                if (transaction is not null)
                    transaction.Rollback();

                return Error.Failure(string.Empty, ex.Message);
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
            IDbConnection dbConnection = _dapperContext.GetConnection();

            IDbTransaction? transaction = null;

            try
            {
                dbConnection.Open();

                transaction = _dapperContext.CreateTransaction();

                var querySql = @"update Task set Deleted = 1, UpdatedAt = @updatedAt where Id = @id";

                await _dapperContext.ExecuteAsync(dbConnection, new CommandDefinition(querySql, new { id, updatedAt = DateTime.Now }, transaction, cancellationToken: ct));

                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                if (transaction is not null)
                    transaction.Rollback();

                return Error.Failure(string.Empty, ex.Message);
            }
            finally
            {
                if (dbConnection.State is ConnectionState.Open)
                    dbConnection.Close();
            }
        }

        public async Task<ErrorOr<IList<TaskDto>>> GetAllAsync(CancellationToken ct)
        {
            IDbConnection dbConnection = _dapperContext.GetConnection();

            try
            {
                dbConnection.Open();

                var querySql = @"select * from Task where Deleted = 0 order by EstimateStartDate asc";

                var results = await _dapperContext.QueryAsync<TaskDto>(dbConnection, new CommandDefinition(querySql, cancellationToken: ct));

                return results.ToList();
            }
            catch (Exception ex)
            {
                return Error.Failure(string.Empty, ex.Message);
            }
            finally
            {
                if (dbConnection.State is ConnectionState.Open)
                    dbConnection.Close();
            }
        }

        public async Task<ErrorOr<TaskDto>> GetByIdAsync(int id, CancellationToken ct)
        {
            IDbConnection dbConnection = _dapperContext.GetConnection();

            try
            {
                dbConnection.Open();

                var querySql = @"select * from Task where Deleted = 0 and Id = @id";

                var result = await _dapperContext.QueryFirstOrDefaultAsync<TaskDto>(dbConnection, new CommandDefinition(querySql, new { id }, cancellationToken: ct));

                if (result is null)
                    return Error.NotFound();

                return result;
            }
            catch (Exception ex)
            {
                return Error.Failure(string.Empty, ex.Message);
            }
            finally
            {
                if (dbConnection.State is ConnectionState.Open)
                    dbConnection.Close();
            }
        }

        public async Task<ErrorOr<bool>> UpdateAsync(TaskDto dto, CancellationToken ct)
        {
            IDbConnection dbConnection = _dapperContext.GetConnection();

            IDbTransaction? transaction = null;

            try
            {
                dbConnection.Open();

                transaction = _dapperContext.CreateTransaction();

                var querySql = @"update Task set Title = @title, Description = @description, EstimateStartDate = @estimateStartDate, EstimateEndDate = @estimateEndDate, UpdatedAt = @updatedAt where Id = @id";

                await _dapperContext.ExecuteAsync(dbConnection, new CommandDefinition(querySql, dto, transaction, cancellationToken: ct));

                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                if (transaction is not null)
                    transaction.Rollback();

                return Error.Failure(string.Empty, ex.Message);
            }
            finally
            {
                if (dbConnection.State is ConnectionState.Open)
                    dbConnection.Close();
            }
        }
    }
}


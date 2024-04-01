using Dapper;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Data;
using System.Data.Common;
using Thunders.Tasks.Core.Data;
using Thunders.Tasks.Core.Dtos;
using Thunders.Tasks.Infra.Repositories;
using Thunders.Tasks.Tests.Builders.Dtos;

namespace Thunders.Tasks.Tests.Infra.Repositories
{
    public class TaskRepositoryTests
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        public TaskRepositoryTests()
        {

            _cancellationTokenSource = new CancellationTokenSource();   
        }

        [Fact]
        public async Task TaskRepositoryTests_Create_Should_Ok()
        {
            int? taskId = GeneralDataBuilder.ID_VALID;

            var dapperContext = Substitute.For<IDapperContext>();            

            DbConnection? connection = Substitute.For<DbConnection>();

            dapperContext.GetConnection().Returns(connection);

            dapperContext.CreateTransaction().Returns(Substitute.For<IDbTransaction>());

            dapperContext.ExecuteAsync(connection, Arg.Any<CommandDefinition>()).Returns(Task.FromResult(true));

            dapperContext.QueryFirstOrDefaultAsync<int?>(connection, Arg.Any<CommandDefinition>())
                .Returns(Task.FromResult(taskId));

            var taskRepository = new TaskRepository(dapperContext);

            var result = await taskRepository.CreateAsync(new TaskDtoBuilder().Build(), _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task TaskRepositoryTests_Create_Should_Error()
        {
            int? taskId = GeneralDataBuilder.ID_VALID;

            var dapperContext = Substitute.For<IDapperContext>();

            DbConnection? connection = Substitute.For<DbConnection>();

            dapperContext.GetConnection().Returns(connection);

            dapperContext.CreateTransaction().Returns(Substitute.For<IDbTransaction>());

            dapperContext.ExecuteAsync(connection, Arg.Any<CommandDefinition>()).Throws<Exception>();

            dapperContext.QueryFirstOrDefaultAsync<int?>(connection, Arg.Any<CommandDefinition>())
                .Returns(Task.FromResult(taskId));

            var taskRepository = new TaskRepository(dapperContext);

            var result = await taskRepository.CreateAsync(new TaskDtoBuilder().Build(), _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.Failure);
        }

        [Fact]
        public async Task TaskRepositoryTests_Update_Should_Ok()
        {
            int? taskId = GeneralDataBuilder.ID_VALID;

            var dapperContext = Substitute.For<IDapperContext>();

            DbConnection? connection = Substitute.For<DbConnection>();

            dapperContext.GetConnection().Returns(connection);

            dapperContext.CreateTransaction().Returns(Substitute.For<IDbTransaction>());

            dapperContext.ExecuteAsync(connection, Arg.Any<CommandDefinition>()).Returns(Task.FromResult(true));

            var taskRepository = new TaskRepository(dapperContext);

            var result = await taskRepository.UpdateAsync(new TaskDtoBuilder().Build(), _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task TaskRepositoryTests_Update_Should_Error()
        {
            int? taskId = GeneralDataBuilder.ID_VALID;

            var dapperContext = Substitute.For<IDapperContext>();

            DbConnection? connection = Substitute.For<DbConnection>();

            dapperContext.GetConnection().Returns(connection);

            dapperContext.CreateTransaction().Returns(Substitute.For<IDbTransaction>());

            dapperContext.ExecuteAsync(connection, Arg.Any<CommandDefinition>()).Throws<Exception>();

            var taskRepository = new TaskRepository(dapperContext);

            var result = await taskRepository.UpdateAsync(new TaskDtoBuilder().Build(), _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.Failure);
        }


        [Fact]
        public async Task TaskRepositoryTests_Delete_Should_Ok()
        {
            var dapperContext = Substitute.For<IDapperContext>();

            DbConnection? connection = Substitute.For<DbConnection>();

            dapperContext.GetConnection().Returns(connection);

            dapperContext.CreateTransaction().Returns(Substitute.For<IDbTransaction>());

            dapperContext.ExecuteAsync(connection, Arg.Any<CommandDefinition>()).Returns(Task.FromResult(true));

            var taskRepository = new TaskRepository(dapperContext);

            var result = await taskRepository.DeleteAsync(GeneralDataBuilder.ID_VALID, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task TaskRepositoryTests_Delete_Should_Error()
        {
            var dapperContext = Substitute.For<IDapperContext>();

            DbConnection? connection = Substitute.For<DbConnection>();

            dapperContext.GetConnection().Returns(connection);

            dapperContext.CreateTransaction().Returns(Substitute.For<IDbTransaction>());

            dapperContext.ExecuteAsync(connection, Arg.Any<CommandDefinition>()).Throws<Exception>();

            var taskRepository = new TaskRepository(dapperContext);

            var result = await taskRepository.DeleteAsync(GeneralDataBuilder.ID_VALID, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.Failure);
        }

        [Fact]
        public async Task TaskRepositoryTests_GetById_Should_Ok()
        {
            var dapperContext = Substitute.For<IDapperContext>();

            DbConnection? connection = Substitute.For<DbConnection>();

            dapperContext.GetConnection().Returns(connection);

            dapperContext.QueryFirstOrDefaultAsync<TaskDto>(connection, Arg.Any<CommandDefinition>()).Returns(new TaskDtoBuilder().Build());

            var taskRepository = new TaskRepository(dapperContext);

            var result = await taskRepository.GetByIdAsync(GeneralDataBuilder.ID_VALID, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task TaskRepositoryTests_GetById_Should_Error()
        {
            var dapperContext = Substitute.For<IDapperContext>();

            DbConnection? connection = Substitute.For<DbConnection>();

            dapperContext.GetConnection().Returns(connection);

            dapperContext.QueryFirstOrDefaultAsync<TaskDto>(connection, Arg.Any<CommandDefinition>()).Throws<Exception>();

            var taskRepository = new TaskRepository(dapperContext);

            var result = await taskRepository.GetByIdAsync(GeneralDataBuilder.ID_VALID, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.Failure);
        }

        [Fact]
        public async Task TaskRepositoryTests_GetAll_Should_Ok()
        {
            var dapperContext = Substitute.For<IDapperContext>();

            DbConnection? connection = Substitute.For<DbConnection>();

            dapperContext.GetConnection().Returns(connection);

            dapperContext.QueryAsync<TaskDto>(connection, Arg.Any<CommandDefinition>()).Returns(new TaskDtoBuilder().Build(1));

            var taskRepository = new TaskRepository(dapperContext);

            var result = await taskRepository.GetAllAsync( _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task TaskRepositoryTests_GetAll_Should_Error()
        {
            var dapperContext = Substitute.For<IDapperContext>();

            DbConnection? connection = Substitute.For<DbConnection>();

            dapperContext.GetConnection().Returns(connection);

            dapperContext.QueryAsync<TaskDto>(connection, Arg.Any<CommandDefinition>()).Throws<Exception>();

            var taskRepository = new TaskRepository(dapperContext);

            var result = await taskRepository.GetAllAsync(_cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.Failure);
        }
    }   
}

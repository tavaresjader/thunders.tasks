using Microsoft.Extensions.Logging;
using NSubstitute;
using Thunders.Tasks.Application.UpdateTask.Handlers;
using Thunders.Tasks.Core.Dtos;
using Thunders.Tasks.Core.Services;
using Thunders.Tasks.Tests.Builders.Application;
using Thunders.Tasks.Tests.Builders.Dtos;

namespace Thunders.Tasks.Tests.Application.UpdateTask.Handlers
{
    public class UpdateTaskCommandHandlerTests
    {
        private ITaskService _taskService;
        private readonly ILogger<UpdateTaskCommandHandler> _logger;
        private readonly UpdateTaskCommandHandler _handler;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public UpdateTaskCommandHandlerTests()
        {
            _taskService = Substitute.For<ITaskService>();
            _logger = Substitute.For<ILogger<UpdateTaskCommandHandler>>();
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new UpdateTaskCommandHandler(_taskService, _logger);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_Should_Ok()
        {
            var request = new UpdateTaskCommandRequestBuilder().Build();

            _taskService.UpdateAsync(Arg.Any<TaskDto>(), _cancellationTokenSource.Token).Returns(true);
            _taskService.GetByIdAsync(request.Id, _cancellationTokenSource.Token).Returns(new TaskDtoBuilder().Build());
           
            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_TaskService_Results_Empty_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder().Build();

            _taskService.UpdateAsync(Arg.Any<TaskDto>(), _cancellationTokenSource.Token).Returns(true);
            _taskService.GetByIdAsync(request.Id, _cancellationTokenSource.Token).Returns(new TaskDtoBuilder().BuildNull());
           
            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.NotFound);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_TaskService_Results_False_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder().Build();

            _taskService.UpdateAsync(Arg.Any<TaskDto>(), _cancellationTokenSource.Token).Returns(false);
            _taskService.GetByIdAsync(request.Id, _cancellationTokenSource.Token).Returns(new TaskDtoBuilder().Build());

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.Failure);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_Id_Invalid_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithId(GeneralDataBuilder.ID_INVALID)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_Title_Empty_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithTitle(GeneralDataBuilder.TEXT_EMPTY)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_Description_Empty_Should_Ok()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithDescription(GeneralDataBuilder.TEXT_EMPTY)
                .Build();

            _taskService.UpdateAsync(Arg.Any<TaskDto>(), _cancellationTokenSource.Token).Returns(true);
            _taskService.GetByIdAsync(request.Id, _cancellationTokenSource.Token).Returns(new TaskDtoBuilder().Build());

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_EstimateStartDate_Empty_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(GeneralDataBuilder.DATETIME_EMPTY)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_EstimateStartDate_MinValue_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.MinValue)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_EstimateStartDate_MaxValue_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.MaxValue)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_EstimateStartDate_PastDate_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.Now.AddDays(-1))
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_EstimateEndDate_Empty_Should_Ok()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateEndDate(GeneralDataBuilder.DATETIME_EMPTY)
                .Build();

            _taskService.UpdateAsync(Arg.Any<TaskDto>(), _cancellationTokenSource.Token).Returns(true);
            _taskService.GetByIdAsync(request.Id, _cancellationTokenSource.Token).Returns(new TaskDtoBuilder().Build());

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_EstimateEndDate_MinValue_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateEndDate(DateTime.MinValue)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_EstimateEndDate_MaxValue_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateEndDate(DateTime.MaxValue)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_EstimateEndDate_PastDate_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateEndDate(DateTime.Now.AddDays(-1))
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_EstimateEndDate_LessThan_EstimateEndDate_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.Now.AddDays(-1))
                .WithEstimateEndDate(DateTime.Now.AddDays(-2))
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task UpdateTaskCommandHandlerTests_EstimateStartDate_GreaterThan_EstimateEndDate_Should_Invalid()
        {
            var request = new UpdateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.Now.AddDays(1))
                .WithEstimateEndDate(DateTime.Now.AddDays(-1))
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }
    }
}

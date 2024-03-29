using Microsoft.Extensions.Logging;
using NSubstitute;
using Thunders.Tasks.Application.DeleteTaskById.Handlers;
using Thunders.Tasks.Core.Dtos;
using Thunders.Tasks.Core.Services;
using Thunders.Tasks.Tests.Builders;
using Thunders.Tasks.Tests.Builders.Dtos;

namespace Thunders.Tasks.Tests.Application.DeleteTaskById.Handlers
{
    public class DeleteTaskByIdCommandHandlerTests
    {
        private ITaskService _taskService;
        private readonly ILogger<DeleteTaskByIdCommandHandler> _logger;
        private readonly DeleteTaskByIdCommandHandler _handler;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public DeleteTaskByIdCommandHandlerTests()
        {
            _taskService = Substitute.For<ITaskService>();
            _logger = Substitute.For<ILogger<DeleteTaskByIdCommandHandler>>();
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new DeleteTaskByIdCommandHandler(_taskService, _logger);
        }

        [Fact]
        public async Task DeleteTaskByIdCommandHandlerTests_Should_Ok()
        {
            _taskService.GetByIdAsync(Arg.Any<int>(), _cancellationTokenSource.Token).ReturnsForAnyArgs(new TaskDtoBuilder().Build());

            var request = new DeleteTaskByIdCommandRequestBuilder().Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task DeleteTaskByIdCommandHandlerTests_Should_Id_Empty_Invalid()
        {
            var request = new DeleteTaskByIdCommandRequestBuilder()
                .WithId(GeneralDataBuilder.ID_EMPTY)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task DeleteTaskByIdCommandHandlerTests_Should_Id_Invalid()
        {
            var request = new DeleteTaskByIdCommandRequestBuilder()
                .WithId(GeneralDataBuilder.ID_INVALID)
                .Build();
            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task DeleteTaskByIdCommandHandlerTests_Should_Task_NotFound_Invalid()
        {        
            _taskService.GetByIdAsync(Arg.Any<int>(), _cancellationTokenSource.Token).ReturnsForAnyArgs(new TaskDtoBuilder().BuildNull());

            var request = new DeleteTaskByIdCommandRequestBuilder()
                .WithId(GeneralDataBuilder.ID_VALID)
                .Build();
            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.NotFound);
        }
    }
}

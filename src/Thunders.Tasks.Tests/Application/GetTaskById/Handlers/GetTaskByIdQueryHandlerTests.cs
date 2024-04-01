using Microsoft.Extensions.Logging;
using NSubstitute;
using Thunders.Tasks.Application.GetTaskById.Handlers;
using Thunders.Tasks.Core.Services;
using Thunders.Tasks.Tests.Builders.Application;
using Thunders.Tasks.Tests.Builders.Dtos;

namespace Thunders.Tasks.Tests.Application.GetTaskById.Handlers
{
    public class GetTaskByIdQueryHandlerTests
    {
        private ITaskService _taskService;
        private readonly ILogger<GetTaskByIdQueryHandler> _logger;
        private readonly GetTaskByIdQueryHandler _handler;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public GetTaskByIdQueryHandlerTests()
        {
            _taskService = Substitute.For<ITaskService>();
            _logger = Substitute.For<ILogger<GetTaskByIdQueryHandler>>();
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new GetTaskByIdQueryHandler(_taskService, _logger);
        }

        [Fact]
        public async Task GetTaskByIdQueryHandlerTests_Should_Ok()
        {
            var request = new GetTaskByIdQueryRequestBuilder().Build();

            _taskService.GetByIdAsync(request.Id, _cancellationTokenSource.Token).Returns(new TaskDtoBuilder().Build());
           
            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task GetTaskByIdQueryHandlerTests_TaskService_Result_Empty_Should_InValid()
        {
            var request = new GetTaskByIdQueryRequestBuilder().Build();

            _taskService.GetByIdAsync(request.Id, _cancellationTokenSource.Token).Returns(new TaskDtoBuilder().BuildNull());

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.NotFound);
        }


        [Fact]
        public async Task GetTaskByIdQueryHandlerTests_Should_Id_Empty_Invalid()
        {
            var request = new GetTaskByIdQueryRequestBuilder()
                .WithId(GeneralDataBuilder.ID_EMPTY)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task GetTaskByIdQueryHandlerTests_Should_Id_Invalid()
        {
            var request = new GetTaskByIdQueryRequestBuilder()
                .WithId(GeneralDataBuilder.ID_INVALID)
                .Build();
            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }
    }
}

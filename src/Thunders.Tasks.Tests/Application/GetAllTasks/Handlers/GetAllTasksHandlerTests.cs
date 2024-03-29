using Microsoft.Extensions.Logging;
using NSubstitute;
using Thunders.Tasks.Application.GetAllTasks.Handlers;
using Thunders.Tasks.Core.Dtos;
using Thunders.Tasks.Core.Services;
using Thunders.Tasks.Tests.Builders.Application;
using Thunders.Tasks.Tests.Builders.Dtos;

namespace Thunders.Tasks.Tests.Application.GetAllTasks.Handlers
{
    public class GetAllTasksHandlerTests
    {
        private ITaskService _taskService;
        private readonly ILogger<GetAllTasksHandler> _logger;
        private readonly GetAllTasksHandler _handler;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public GetAllTasksHandlerTests()
        {
            _taskService = Substitute.For<ITaskService>();
            _logger = Substitute.For<ILogger<GetAllTasksHandler>>();
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new GetAllTasksHandler(_taskService, _logger);
        }

        [Fact]
        public async Task GetAllTasksHandlerTests_Should_Ok()
        {
            int listSize = 10;

            _taskService.GetAllAsync(_cancellationTokenSource.Token).Returns(new TaskDtoBuilder().Build(listSize).ToList());

            var request = new GetAllTasksQueryRequestBuilder().Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
            Assert.NotNull(result.Value);
            Assert.Equal(result!.Value!.Count, listSize);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_TaskService_Results_Empty_Should_Valid()
        {
            int listSize = 0;

            _taskService.GetAllAsync(_cancellationTokenSource.Token).Returns(new TaskDtoBuilder().Build(listSize).ToList());

            var request = new GetAllTasksQueryRequestBuilder().Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
            Assert.NotNull(result.Value);
            Assert.Equal(result!.Value!.Count, listSize);
        }
    }
}

using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.Tasks.Application.CreateTask.Handlers;
using Thunders.Tasks.Application.CreateTask.Validators;
using Thunders.Tasks.Core.Dtos;
using Thunders.Tasks.Core.Services;
using Thunders.Tasks.Tests.Builders;
using Thunders.Tasks.Tests.Builders.Application;

namespace Thunders.Tasks.Tests.Application.CreateTask.Handlers
{
    public class CreateTaskCommandHandlerTests
    {
        private ITaskService _taskService;
        private readonly ILogger<CreateTaskCommandHandler> _logger;
        private readonly CreateTaskCommandHandler _handler;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public CreateTaskCommandHandlerTests()
        {
            _taskService = Substitute.For<ITaskService>();
            _logger = Substitute.For<ILogger<CreateTaskCommandHandler>>();
            _cancellationTokenSource = new CancellationTokenSource();
            _handler = new CreateTaskCommandHandler(_taskService, _logger);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_Should_Ok()
        {
            var request = new CreateTaskCommandRequestBuilder().Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_TaskService_Results_Zero_Should_Invalid()
        {
            _taskService.CreateAsync(Arg.Any<TaskDto>(), _cancellationTokenSource.Token).Returns(0);

            var request = new CreateTaskCommandRequestBuilder().Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
            Assert.Contains(result.Errors, w => w.Type == ErrorOr.ErrorType.Failure);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_Title_Empty_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithTitle(GeneralDataBuilder.TEXT_EMPTY)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_Description_Empty_Should_Ok()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithDescription(GeneralDataBuilder.TEXT_EMPTY)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_EstimateStartDate_Empty_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(GeneralDataBuilder.DATETIME_EMPTY)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_EstimateStartDate_MinValue_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.MinValue)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_EstimateStartDate_MaxValue_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.MaxValue)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_EstimateStartDate_PastDate_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.Now.AddDays(-1))
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_EstimateEndDate_Empty_Should_Ok()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateEndDate(GeneralDataBuilder.DATETIME_EMPTY)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.False(result.IsError);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_EstimateEndDate_MinValue_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateEndDate(DateTime.MinValue)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_EstimateEndDate_MaxValue_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateEndDate(DateTime.MaxValue)
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_EstimateEndDate_PastDate_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateEndDate(DateTime.Now.AddDays(-1))
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_EstimateEndDate_LessThan_EstimateEndDate_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.Now.AddDays(-1))
                .WithEstimateEndDate(DateTime.Now.AddDays(-2))
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }

        [Fact]
        public async Task CreateTaskCommandHandlerTests_EstimateStartDate_GreaterThan_EstimateEndDate_Should_Invalid()
        {
            var request = new CreateTaskCommandRequestBuilder()
                .WithEstimateStartDate(DateTime.Now.AddDays(1))
                .WithEstimateEndDate(DateTime.Now.AddDays(-1))
                .Build();

            var result = await _handler.Handle(request, _cancellationTokenSource.Token);

            Assert.True(result.IsError);
        }
    }
}

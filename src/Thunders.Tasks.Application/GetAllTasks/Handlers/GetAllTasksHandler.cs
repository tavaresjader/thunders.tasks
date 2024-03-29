using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Thunders.Tasks.Application.GetAllTasks.Builders;
using Thunders.Tasks.Application.GetAllTasks.Queries;
using Thunders.Tasks.Application.UpdateTask.Handlers;
using Thunders.Tasks.Core.Services;

namespace Thunders.Tasks.Application.GetAllTasks.Handlers
{
    public class GetAllTasksHandler : IRequestHandler<GetAllTasksQueryRequest, ErrorOr<IList<GetAllTasksQueryResponse>>>
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<GetAllTasksHandler> _logger;
        private const string HANDLER_NAME = nameof(GetAllTasksHandler);

        public GetAllTasksHandler(ITaskService taskService, ILogger<GetAllTasksHandler> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        public async Task<ErrorOr<IList<GetAllTasksQueryResponse>>> Handle(GetAllTasksQueryRequest request, CancellationToken ct)
        {
            try
            {
                _logger.LogInformation($"Handler {HANDLER_NAME} started");

                var getAllTasksResult = await _taskService.GetAllAsync(ct);

                if (getAllTasksResult.IsError)
                    return getAllTasksResult.Errors;

                var result = GetAllTasksBuilder.Build(getAllTasksResult.Value);

                _logger.LogInformation($"Handler {HANDLER_NAME} finished");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Handler {HANDLER_NAME} Error");

                return Error.Failure("", $"Fail to Delete Task {ex.Message}");
            }

        }
    }
}

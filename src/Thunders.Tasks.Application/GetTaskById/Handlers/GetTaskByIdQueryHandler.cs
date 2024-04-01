using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Thunders.Tasks.Application.GetTaskById.Builders;
using Thunders.Tasks.Application.GetTaskById.Queries;
using Thunders.Tasks.Application.GetTaskById.Validators;
using Thunders.Tasks.Application.UpdateTask.Handlers;
using Thunders.Tasks.Core.Extensions;
using Thunders.Tasks.Core.Services;

namespace Thunders.Tasks.Application.GetTaskById.Handlers
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQueryRequest, ErrorOr<GetTaskByIdQueryResponse>>
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<GetTaskByIdQueryHandler> _logger;
        private const string HANDLER_NAME = nameof(GetTaskByIdQueryHandler);

        public GetTaskByIdQueryHandler(ITaskService taskService, ILogger<GetTaskByIdQueryHandler> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        public async Task<ErrorOr<GetTaskByIdQueryResponse>> Handle(GetTaskByIdQueryRequest request, CancellationToken ct)
        {
            try
            {
                _logger.LogInformation($"Handler {HANDLER_NAME} started");

                var validation = await new GetTaskByIdQueryRequestValidator().ValidateAsync(request, ct);

                if (!validation.IsValid)
                    return validation.Errors.ToValidation();

                var taskGetByIdResult = await _taskService.GetByIdAsync(request.Id, ct);

                if (taskGetByIdResult.IsError)
                    return taskGetByIdResult.Errors;

                if (taskGetByIdResult.Value is null)
                    return Error.NotFound("", $"Task {request.Id} Not Found");

                var result = GetTaskByIdBuilder.Build(taskGetByIdResult.Value);

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

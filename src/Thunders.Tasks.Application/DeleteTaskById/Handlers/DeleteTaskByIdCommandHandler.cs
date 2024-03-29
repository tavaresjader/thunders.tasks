using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Thunders.Tasks.Application.DeleteTaskById.Builders;
using Thunders.Tasks.Application.DeleteTaskById.Commands;
using Thunders.Tasks.Application.DeleteTaskById.Validators;
using Thunders.Tasks.Application.UpdateTask.Handlers;
using Thunders.Tasks.Core.Extensions;
using Thunders.Tasks.Core.Services;

namespace Thunders.Tasks.Application.DeleteTaskById.Handlers
{
    public class DeleteTaskByIdCommandHandler : IRequestHandler<DeleteTaskByIdCommandRequest, ErrorOr<DeleteTaskByIdCommandResponse>>
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<DeleteTaskByIdCommandHandler> _logger;
        private const string HANDLER_NAME = nameof(DeleteTaskByIdCommandHandler);

        public DeleteTaskByIdCommandHandler(ITaskService taskService, ILogger<DeleteTaskByIdCommandHandler> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        public async Task<ErrorOr<DeleteTaskByIdCommandResponse>> Handle(DeleteTaskByIdCommandRequest request, CancellationToken ct)
        {
            try
            {
                _logger.LogInformation($"Handler {HANDLER_NAME} started");

                var validation = await new DeleteTaskByIdCommandRequestValidator().ValidateAsync(request, ct);

                if (!validation.IsValid)
                    return validation.Errors.ToValidation();

                var taskGetByIdResult = await _taskService.GetByIdAsync(request.Id, ct);

                if (taskGetByIdResult.IsError)
                    return taskGetByIdResult.Errors;

                if (taskGetByIdResult.Value is null)
                    return Error.NotFound("", $"Task {request.Id} Not Found");

                var deleteTaskResult = await _taskService.DeleteAsync(request.Id, ct);

                if (deleteTaskResult.IsError)
                    return deleteTaskResult.Errors;

                _logger.LogInformation($"Handler {HANDLER_NAME} finished");

                return DeleteTaskBuilder.Build();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Handler {HANDLER_NAME} Error");

                return Error.Failure("", $"Fail to Delete Task {ex.Message}");
            }
        }
    }
}

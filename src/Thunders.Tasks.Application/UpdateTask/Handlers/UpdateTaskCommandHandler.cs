using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Thunders.Tasks.Application.UpdateTask.Builders;
using Thunders.Tasks.Application.UpdateTask.Commands;
using Thunders.Tasks.Application.UpdateTask.Validators;
using Thunders.Tasks.Core.Extensions;
using Thunders.Tasks.Core.Services;

namespace Thunders.Tasks.Application.UpdateTask.Handlers
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommandRequest, ErrorOr<UpdateTaskCommandResponse>>
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<UpdateTaskCommandHandler> _logger;
        private const string HANDLER_NAME = nameof(UpdateTaskCommandHandler);

        public UpdateTaskCommandHandler(ITaskService taskService, ILogger<UpdateTaskCommandHandler> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        public async Task<ErrorOr<UpdateTaskCommandResponse>> Handle(UpdateTaskCommandRequest request, CancellationToken ct)
        {
            try
            {
                _logger.LogInformation($"Handler {HANDLER_NAME} started");

                var validation = await new UpdateTaskCommandRequestValidator().ValidateAsync(request, ct);

                if (!validation.IsValid)
                    return validation.Errors.ToValidation();

                var taskGetByIdResult = await _taskService.GetByIdAsync(request.Id, ct);

                if (taskGetByIdResult.IsError)
                    return taskGetByIdResult.Errors;

                if (taskGetByIdResult.Value is null)
                    return Error.NotFound("", $"Task {request.Id} Not Found");

                var taskDto = UpdateTaskBuilder.Build(request);

                var updateTaskResult = await _taskService.UpdateAsync(taskDto, ct);

                if (updateTaskResult.IsError)
                    return updateTaskResult.Errors;

                if (updateTaskResult.Value is false)
                    return Error.Failure("", $"Task {request.Id} Failure at Update");

                _logger.LogInformation($"Handler {HANDLER_NAME} finished");

                return UpdateTaskBuilder.Build();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Handler {HANDLER_NAME} Error");

                return Error.Failure("", $"Fail to Update Task {ex.Message}");
            }            
        }
    }
}

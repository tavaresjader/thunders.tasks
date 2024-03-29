using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Thunders.Tasks.Application.CreateTask.Builders;
using Thunders.Tasks.Application.CreateTask.Commands;
using Thunders.Tasks.Application.CreateTask.Validators;
using Thunders.Tasks.Core.Extensions;
using Thunders.Tasks.Core.Services;

namespace Thunders.Tasks.Application.CreateTask.Handlers
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommandRequest, ErrorOr<CreateTaskCommandResponse>>
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<CreateTaskCommandHandler> _logger;
        private const string HANDLER_NAME = nameof(CreateTaskCommandHandler);

        public CreateTaskCommandHandler(ITaskService taskService, ILogger<CreateTaskCommandHandler> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        public async Task<ErrorOr<CreateTaskCommandResponse>> Handle(CreateTaskCommandRequest request, CancellationToken ct)
        {            
            try
            {
                _logger.LogInformation($"Handler {HANDLER_NAME} started");

                var validation = await new CreateTaskCommandRequestValidator().ValidateAsync(request, ct);

                if (!validation.IsValid)
                    return validation.Errors.ToValidation();

                var taskDto = CreateTaskBuilder.Build(request);

                var createTaskResult = await _taskService.CreateAsync(taskDto, ct);

                if (createTaskResult.IsError)
                    return createTaskResult.Errors;

                if (createTaskResult.Value is 0)
                    return Error.Failure();

                var result = CreateTaskBuilder.Build(createTaskResult.Value);

                _logger.LogInformation($"Handler {HANDLER_NAME} finished");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Handler {HANDLER_NAME} Error");

                return Error.Failure("", $"Fail to Create Task {ex.Message}");
            }
        }
    }
}

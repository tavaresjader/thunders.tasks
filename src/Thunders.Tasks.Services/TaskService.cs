using ErrorOr;
using Thunders.Tasks.Core.Dtos;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Core.Services;

namespace Thunders.Tasks.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ErrorOr<int>> CreateAsync(TaskDto dto, CancellationToken ct)
        {
            var result = await _taskRepository.CreateAsync(dto, ct);

            if (result.IsError)
                return result.Errors;

            if (result.Value <= 0)
                return Error.Failure("", "Failure at Create Task");

            return result.Value;
        }

        public async Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken ct)
        {
            var result = await _taskRepository.DeleteAsync(id, ct);

            if (result.IsError)
                return result.Errors;

            if (!result.Value)
                return Error.Failure("", "Failure at Delete Task");

            return result.Value;
        }

        public async Task<ErrorOr<IList<TaskDto>>> GetAllAsync(CancellationToken ct)
        {
            var result = await _taskRepository.GetAllAsync(ct);

            if (result.IsError)
                return result.Errors;

            return result.Value.ToList();
        }

        public async Task<ErrorOr<TaskDto>> GetByIdAsync(int id, CancellationToken ct)
        {
            var result = await _taskRepository.GetByIdAsync(id, ct);

            if (result.Value is null)
                return Error.NotFound("", "Task Not Found");

            if (result.IsError)
                return result.Errors;

            return result.Value;
        }

        public async Task<ErrorOr<bool>> UpdateAsync(TaskDto dto, CancellationToken ct)
        {
            var result = await _taskRepository.UpdateAsync(dto, ct);

            if (result.IsError)
                return result.Errors;

            if (!result.Value)
                return Error.Failure("", "Failure at Update Task");

            return result.Value;
        }
    }
}

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
            return await _taskRepository.CreateAsync(dto, ct);
        }

        public async Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken ct)
        {
            return await _taskRepository.DeleteAsync(id, ct);
        }

        public async Task<ErrorOr<IList<TaskDto>>> GetAllAsync(CancellationToken ct)
        {
            return await _taskRepository.GetAllAsync(ct);
        }

        public async Task<ErrorOr<TaskDto>> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _taskRepository.GetByIdAsync(id, ct);
        }

        public async Task<ErrorOr<bool>> UpdateAsync(TaskDto dto, CancellationToken ct)
        {
            return await _taskRepository.UpdateAsync(dto, ct);
        }
    }
}

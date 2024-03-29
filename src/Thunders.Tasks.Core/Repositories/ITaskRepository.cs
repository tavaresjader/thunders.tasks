using ErrorOr;
using Thunders.Tasks.Core.Dtos;

namespace Thunders.Tasks.Core.Repositories
{
    public interface ITaskRepository
    {
        Task<ErrorOr<int>> CreateAsync(TaskDto dto, CancellationToken ct);
        Task<ErrorOr<bool>> UpdateAsync(TaskDto dto, CancellationToken ct);
        Task<ErrorOr<bool>> DeleteAsync(int id, CancellationToken ct);
        Task<ErrorOr<TaskDto>> GetByIdAsync(int id, CancellationToken ct);
        Task<ErrorOr<IList<TaskDto>>> GetAllAsync(CancellationToken ct);
    }
}

using Thunders.Tasks.Application.UpdateTask.Commands;
using Thunders.Tasks.Core.Dtos;

namespace Thunders.Tasks.Application.UpdateTask.Builders
{
    public static class UpdateTaskBuilder
    {
        public static TaskDto Build(UpdateTaskCommandRequest request)
        {
            var dto = new TaskDto();
            dto.Id = request.Id;
            dto.Title = request.Title;
            dto.Description = request.Description;
            dto.EstimateStartDate = request.EstimateStartDate;
            dto.EstimateEndDate = request.EstimateEndDate;
            dto.EstimateEndDate = request.EstimateEndDate;
            dto.UpdatedAt = DateTime.Now;
            return dto;
        }

        public static UpdateTaskCommandResponse Build()
        {
            return new UpdateTaskCommandResponse();
        }
    }
}

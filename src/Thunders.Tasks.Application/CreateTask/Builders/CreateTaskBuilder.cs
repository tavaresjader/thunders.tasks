using Thunders.Tasks.Application.CreateTask.Commands;
using Thunders.Tasks.Core.Dtos;

namespace Thunders.Tasks.Application.CreateTask.Builders
{
    public static class CreateTaskBuilder 
    {
        public static TaskDto Build(CreateTaskCommandRequest request) 
        {
            TaskDto dto = new TaskDto();
            dto.Title = request.Title;
            dto.Description = request.Description;
            dto.EstimateStartDate = request.EstimateStartDate;
            dto.EstimateEndDate = request.EstimateEndDate;
            dto.EstimateEndDate = request.EstimateEndDate;
            dto.CreatedAt = DateTime.Now;
            return dto;
        }
        public static CreateTaskCommandResponse Build(int id) 
        {
            return new CreateTaskCommandResponse(id);
        }

    }
}

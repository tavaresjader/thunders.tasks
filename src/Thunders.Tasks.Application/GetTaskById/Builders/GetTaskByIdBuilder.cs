using Thunders.Tasks.Application.GetTaskById.Queries;
using Thunders.Tasks.Core.Dtos;

namespace Thunders.Tasks.Application.GetTaskById.Builders
{
    public static class GetTaskByIdBuilder
    {
        internal static GetTaskByIdQueryResponse Build(TaskDto dto)
        {
            GetTaskByIdQueryResponse getTaskByIdQueryResponse = new GetTaskByIdQueryResponse();
            getTaskByIdQueryResponse.Id = dto.Id;
            getTaskByIdQueryResponse.Title = dto.Title;
            getTaskByIdQueryResponse.Description = dto.Description;
            getTaskByIdQueryResponse.EstimateStartDate = dto.EstimateStartDate;
            getTaskByIdQueryResponse.EstimateEndDate = dto.EstimateEndDate;            
            getTaskByIdQueryResponse.CreatedAt = dto.CreatedAt;
            getTaskByIdQueryResponse.UpdatedAt = dto.UpdatedAt;
            return getTaskByIdQueryResponse;
        }
    }
}

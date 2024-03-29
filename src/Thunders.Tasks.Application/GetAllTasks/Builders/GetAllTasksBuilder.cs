using Thunders.Tasks.Application.GetAllTasks.Queries;
using Thunders.Tasks.Core.Dtos;

namespace Thunders.Tasks.Application.GetAllTasks.Builders
{
    public static class GetAllTasksBuilder
    {
        public static List<GetAllTasksQueryResponse> Build(IList<TaskDto> dtos)
        {
            var results = new List<GetAllTasksQueryResponse>();

            if(dtos is not null)
            {
                foreach (var dto in dtos)
                {
                    GetAllTasksQueryResponse getAllTasksQueryResponse = new GetAllTasksQueryResponse();
                    getAllTasksQueryResponse.Id = dto.Id;
                    getAllTasksQueryResponse.Title = dto.Title;
                    getAllTasksQueryResponse.Description = dto.Description;
                    getAllTasksQueryResponse.EstimateStartDate = dto.EstimateStartDate;
                    getAllTasksQueryResponse.EstimateEndDate = dto.EstimateEndDate;
                    getAllTasksQueryResponse.CreatedAt = dto.CreatedAt;
                    getAllTasksQueryResponse.UpdatedAt = dto.UpdatedAt;
                    results.Add(getAllTasksQueryResponse);
                }
            }

            return results;
        }
    }
}

using ErrorOr;
using MediatR;
using System.Text.Json.Serialization;

namespace Thunders.Tasks.Application.UpdateTask.Commands
{
    public class UpdateTaskCommandRequest : IRequest<ErrorOr<UpdateTaskCommandResponse>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int EstimateEffortHours { get; set; }
        public DateTime? EstimateStartDate { get; set; }
        public DateTime? EstimateEndDate { get; set; }

        public void DefineId(int id)
            => Id = id;
    }
}

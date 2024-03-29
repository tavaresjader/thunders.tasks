using ErrorOr;
using MediatR;

namespace Thunders.Tasks.Application.CreateTask.Commands
{
    public class CreateTaskCommandRequest : IRequest<ErrorOr<CreateTaskCommandResponse>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? EstimateStartDate { get; set; }
        public DateTime? EstimateEndDate { get; set; }
    }
}

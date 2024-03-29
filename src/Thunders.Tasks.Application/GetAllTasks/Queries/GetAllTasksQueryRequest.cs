using ErrorOr;
using MediatR;

namespace Thunders.Tasks.Application.GetAllTasks.Queries
{
    public class GetAllTasksQueryRequest : IRequest<ErrorOr<IList<GetAllTasksQueryResponse>>>
    {

    }
}

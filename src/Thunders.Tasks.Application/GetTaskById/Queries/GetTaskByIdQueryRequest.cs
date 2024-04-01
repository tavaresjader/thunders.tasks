using ErrorOr;
using MediatR;

namespace Thunders.Tasks.Application.GetTaskById.Queries
{
    public class GetTaskByIdQueryRequest : IRequest<ErrorOr<GetTaskByIdQueryResponse>>
    {
        public GetTaskByIdQueryRequest()
        {
                
        }

        public GetTaskByIdQueryRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

    }
}

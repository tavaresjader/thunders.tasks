using ErrorOr;
using MediatR;

namespace Thunders.Tasks.Application.DeleteTaskById.Commands
{
    public class DeleteTaskByIdCommandRequest : IRequest<ErrorOr<DeleteTaskByIdCommandResponse>>
    {
        public DeleteTaskByIdCommandRequest()
        {
            
        }
        public DeleteTaskByIdCommandRequest(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

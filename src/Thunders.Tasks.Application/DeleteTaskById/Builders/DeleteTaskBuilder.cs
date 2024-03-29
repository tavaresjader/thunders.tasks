using Thunders.Tasks.Application.DeleteTaskById.Commands;

namespace Thunders.Tasks.Application.DeleteTaskById.Builders
{
    public static class DeleteTaskBuilder
    {
        public static DeleteTaskByIdCommandResponse Build()
        {
            return new();
        }
    }
}

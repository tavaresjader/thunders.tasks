namespace Thunders.Tasks.Application.CreateTask.Commands
{
    public class CreateTaskCommandResponse
    {
        public CreateTaskCommandResponse(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

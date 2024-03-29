using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Thunders.Tasks.Application.CreateTask.Commands;
using Thunders.Tasks.Application.DeleteTaskById.Commands;
using Thunders.Tasks.Application.GetAllTasks.Queries;
using Thunders.Tasks.Application.GetTaskById.Queries;
using Thunders.Tasks.Application.UpdateTask.Commands;

namespace Thunders.Tasks.Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly IMediator _mediator;

        public TasksController(ILogger<TasksController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateTaskAsync(CreateTaskCommandRequest request, CancellationToken ct)
        {
            var result = await _mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskAsync(UpdateTaskCommandRequest request, [Required] int id, CancellationToken ct)
        {
            request.DefineId(id);

            var result = await _mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAsync([Required] int id, CancellationToken ct)
        {
            var request = new DeleteTaskByIdCommandRequest(id);            

            var result = await _mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskByIdAsync([Required] int id, CancellationToken ct)
        {
            var request = new GetTaskByIdQueryRequest(id);

            var result = await _mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }


        [HttpGet()]
        public async Task<IActionResult> GetAllTaskAssync(CancellationToken ct)
        {
            var request = new GetAllTasksQueryRequest();

            var result = await _mediator.Send(request, ct);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
    }
}

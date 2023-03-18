using GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTask;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using GlobalTask.TaskManagement.Api.Controllers.Base;
using GlobalTask.TaskManagement.Application.Contracts.Notifications;
using GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTaskById;
using GlobalTask.TaskManagement.Application.Features.Tasks.Commands.CreateTask;
using GlobalTask.TaskManagement.Application.Features.Tasks.Commands.UpdateTask;
using System.Net;

namespace Corporation.Register.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ApiControllerBase
    {
        public TaskController(IMediator mediator, INotificationsHandler notificationsHandler):base(mediator, notificationsHandler)
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetTaskResponse>))]
        public async Task<IActionResult> GetAsync()
            => await SendAsync(new GetTaskQuery());

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTaskByIdResponse))]
        public async Task<IActionResult> GetAsync(Guid id)
            => await SendAsync(new GetTaskByIdQuery(id));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(CreateTaskCommand createTaskCommand)
            => await SendAsync(createTaskCommand, HttpStatusCode.Created);

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(UpdateTaskCommand updateTaskCommand)
            => await SendAsync(updateTaskCommand);
    }
}
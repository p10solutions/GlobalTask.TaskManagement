using Microsoft.AspNetCore.Mvc;
using MassTransit;
using GlobalTask.TaskManagement.Application.Models.Events.Tasks;

namespace Corporation.Register.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventTaskController : ControllerBase
    {
        readonly IPublishEndpoint _publisher;

        public EventTaskController(IPublishEndpoint publisher)
        {
            _publisher = publisher;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(TaskInsertedEvent taskInsertedEvent)
        {
            await _publisher.Publish(taskInsertedEvent);

            return Ok(taskInsertedEvent);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(TaskUpdatedEvent taskUpdatedEvent)
        {
            await _publisher.Publish(taskUpdatedEvent);

            return Ok(taskUpdatedEvent);
        }
    }
}
using GlobalTask.TaskManagement.Application.Features.Tasks.Commands.UpdateTask;

namespace GlobalTask.TaskManagement.Application.Models.Events.Tasks.Maps
{
    public static class TaskUpdatedMapper
    {
        public static UpdateTaskCommand MapTo(TaskUpdatedEvent taskUpdatedEvent)
            => new(taskUpdatedEvent.Id, taskUpdatedEvent.Description, taskUpdatedEvent.Date, taskUpdatedEvent.Status);
    }
}

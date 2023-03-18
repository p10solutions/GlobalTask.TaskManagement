using GlobalTask.TaskManagement.Application.Features.Tasks.Commands.CreateTask;

namespace GlobalTask.TaskManagement.Application.Models.Events.Tasks.Maps
{
    public static class TaskInsertedMapper
    {
        public static CreateTaskCommand MapTo(TaskInsertedEvent taskInsertedEvent)
            => new(taskInsertedEvent.Description, taskInsertedEvent.Date, taskInsertedEvent.Status);
    }
}

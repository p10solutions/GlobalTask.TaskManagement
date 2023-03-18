using GlobalTask.TaskManagement.Application.Entities;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandMapper
    {
        public static TaskItem MapTo(UpdateTaskCommand command)
            => new(command.Description, command.Date, command.Status) { Id = command.Id, Active = command.Active };
    }
}

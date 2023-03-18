using GlobalTask.TaskManagement.Application.Entities;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandMapper
    {
        public static TaskItem MapTo(CreateTaskCommand command)
            => new(command.Description, command.Date, command.Status);
    }
}

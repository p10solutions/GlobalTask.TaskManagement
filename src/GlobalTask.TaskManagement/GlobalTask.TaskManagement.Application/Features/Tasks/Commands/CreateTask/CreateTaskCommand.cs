using GlobalTask.TaskManagement.Application.Contracts.Validation;
using GlobalTask.TaskManagement.Application.Entities;
using GlobalTask.TaskManagement.Application.Features.Common;
using MediatR;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommand : CommandBase<CreateTaskCommand>, IRequest<Guid>
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public EStatus Status { get; set; }

        public CreateTaskCommand(string description, DateTime date, EStatus status)
            : base(new CreateTaskCommandValidator())
        {
            Description = description;
            Date = date;
            Status = status;
        }
    }
}

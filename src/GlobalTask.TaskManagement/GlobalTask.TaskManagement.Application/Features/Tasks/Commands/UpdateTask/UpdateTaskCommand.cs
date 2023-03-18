using GlobalTask.TaskManagement.Application.Contracts.Validation;
using GlobalTask.TaskManagement.Application.Entities;
using GlobalTask.TaskManagement.Application.Features.Common;
using MediatR;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommand : CommandBase<UpdateTaskCommand>, IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public EStatus Status { get; set; }
        public bool Active { get; set; }

        public UpdateTaskCommand(Guid id, string description, DateTime date, EStatus status)
            : base(new UpdateTaskCommandValidator())
        {
            Id = id;
            Description = description;
            Date = date;
            Status = status;
        }
    }
}

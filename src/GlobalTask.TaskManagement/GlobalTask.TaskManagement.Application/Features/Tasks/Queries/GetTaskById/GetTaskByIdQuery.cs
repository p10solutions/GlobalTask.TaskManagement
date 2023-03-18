using GlobalTask.TaskManagement.Application.Contracts.Validation;
using GlobalTask.TaskManagement.Application.Entities;
using GlobalTask.TaskManagement.Application.Features.Common;
using MediatR;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdQuery : CommandBase<GetTaskByIdQuery>, IRequest<GetTaskByIdResponse>
    {
        public Guid Id { get; set; }

        public GetTaskByIdQuery(Guid id)
            : base(new GetTaskByIdCommandValidator())
        {
            Id = id;
        }
    }
}

using FluentValidation;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdCommandValidator: AbstractValidator<GetTaskByIdQuery>
    {
        public GetTaskByIdCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
}

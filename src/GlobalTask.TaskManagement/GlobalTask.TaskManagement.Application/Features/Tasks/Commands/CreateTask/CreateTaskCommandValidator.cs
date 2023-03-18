using FluentValidation;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator: AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Description).Length(2, 200).WithMessage("Description only allows up to 200 characters");
        }
    }
}

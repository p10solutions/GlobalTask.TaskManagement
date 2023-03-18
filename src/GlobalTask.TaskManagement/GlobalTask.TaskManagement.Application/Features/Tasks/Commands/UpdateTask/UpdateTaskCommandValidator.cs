using FluentValidation;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Commands.UpdateTask
{
    public class UpdateTaskCommandValidator: AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Description).Length(1, 200).WithMessage("Description only allows up to 200 characters");
        }
    }
}

using FluentValidation;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTask
{
    public class GetTaskQueryValidator: AbstractValidator<GetTaskQuery>
    {
        public GetTaskQueryValidator()
        {
        }
    }
}

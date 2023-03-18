using GlobalTask.TaskManagement.Application.Features.Common;
using MediatR;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTask
{
    public class GetTaskQuery : CommandBase<GetTaskQuery>, IRequest<IEnumerable<GetTaskResponse>>
    {
        public GetTaskQuery()
            : base(new GetTaskQueryValidator())
        {
        }
    }
}

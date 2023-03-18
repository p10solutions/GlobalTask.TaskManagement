using GlobalTask.TaskManagement.Application.Entities;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTask
{
    public class GetTaskMapper
    {
        public static GetTaskResponse MapFrom(TaskItem taskItem)
            => new(taskItem.Id, taskItem.Description, taskItem.Date, taskItem.Status, taskItem.Active);

        public static IEnumerable<GetTaskResponse> MapFrom(IEnumerable<TaskItem> taskItems)
            => taskItems.Select(x => MapFrom(x));
    }
}

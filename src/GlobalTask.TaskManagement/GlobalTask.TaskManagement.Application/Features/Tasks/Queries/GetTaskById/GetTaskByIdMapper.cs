using GlobalTask.TaskManagement.Application.Entities;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdMapper
    {
        public static GetTaskByIdResponse MapFrom(TaskItem taskItem)
            => new(taskItem.Id, taskItem.Description, taskItem.Date, taskItem.Status, taskItem.Active); 
    }
}

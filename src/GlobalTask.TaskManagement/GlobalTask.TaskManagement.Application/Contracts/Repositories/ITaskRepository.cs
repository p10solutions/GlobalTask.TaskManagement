using GlobalTask.TaskManagement.Application.Entities;

namespace GlobalTask.TaskManagement.Application.Contracts.Repositories
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task<TaskItem> GetAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetAsync();
    }
}

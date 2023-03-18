using GlobalTask.TaskManagement.Application.Entities;

namespace GlobalTask.TaskManagement.Application.Models.Events.Tasks
{
    public class TaskUpdatedEvent
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public EStatus Status { get; set; }
        public bool Active { get; set; }

        public TaskUpdatedEvent(Guid id, string description, DateTime date, EStatus status)
        {
            Id = id;
            Description = description;
            Date = date;
            Status = status;
            Active = true;
        }
    }
}

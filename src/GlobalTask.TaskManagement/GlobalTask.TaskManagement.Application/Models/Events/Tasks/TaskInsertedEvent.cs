using GlobalTask.TaskManagement.Application.Entities;

namespace GlobalTask.TaskManagement.Application.Models.Events.Tasks
{
    public class TaskInsertedEvent
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public EStatus Status { get; set; }
        public bool Active { get; set; }

        public TaskInsertedEvent(string description, DateTime date, EStatus status)
        {
            Description = description;
            Date = date;
            Status = status;
            Active = true;
        }
    }
}

namespace GlobalTask.TaskManagement.Application.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public EStatus Status { get; set; }
        public bool Active { get; set; }

        public TaskItem()
        {

        }

        public TaskItem(string description, DateTime date, EStatus status)
        {
            Id = Guid.NewGuid();
            Description = description;
            Date = date;
            Status = status;
            Active = true;
        }
    }
}

using GlobalTask.TaskManagement.Application.Entities;

namespace GlobalTask.TaskManagement.Application.Features.Tasks.Queries.GetTaskById
{
    public class GetTaskByIdResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public EStatus Status { get; set; }
        public bool Active { get; set; }

        public GetTaskByIdResponse(Guid id, string description, DateTime date, EStatus status, bool active)
        {
            Id = id;
            Description = description;
            Date = date;
            Status = status;
            Active = active;
        }
    }
}

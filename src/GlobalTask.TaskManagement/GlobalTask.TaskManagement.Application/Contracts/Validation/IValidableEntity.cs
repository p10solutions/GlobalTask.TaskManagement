namespace GlobalTask.TaskManagement.Application.Contracts.Validation
{
    public interface IValidableEntity
    {
        ISet<string> Errors { get; }
        bool Validate();
    }
}

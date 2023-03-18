namespace GlobalTask.TaskManagement.Application.Models.Notifications
{
    public class Notification
    {
        public string Details { get; set; }
        public ENotificationType NotificationType { get; set; }
        public object Data { get; set; }
    }
}

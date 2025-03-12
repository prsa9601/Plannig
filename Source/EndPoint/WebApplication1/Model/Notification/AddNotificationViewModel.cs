using Domain.Notification;

namespace Planning.Api.Model.Notification
{
    public class AddNotificationViewModel
    {
        public NotificationType NotificationType { get; set; }
        public DateTime SendTime { get; set; }
        public long EventId { get; set; }
        public DateTime EventStartTime { get; set; }
        public List<string> UserIds { get; set; }
    }
}

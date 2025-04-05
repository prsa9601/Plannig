using Common.Query;
using Common.Query.Filter;
using Domain.EventAgg.Enum;
using Domain.Notification;

namespace Query.Notification.DTOs
{
    public class NotificationFilterData : BaseDto
    {
        //public long? EventId { get;  set; }
        public bool IsSend { get;  set; }
        public bool IsActive { get;  set; } //برای وقتی که تایم پکیج تموم شد
        public DateTime EventStartTime { get;  set; }
        public DateTime EventEndTime { get;  set; }
        public DateTime SendTime { get;  set; }
        public bool IsSeen { get; set; }

        public NotificationType NotificationType { get;  set; }
        public ICollection<string>? UserNames { get;  set; }

        public EventNotificationDto? eventDto { get; set; }
    }
    
    public class NotificationFilterParam : BaseFilterParam
    {
        public required string UserName { get; set; }
    }
    public class NotificationFilterParamViewModel : BaseFilterParam
    {
    }
    public class NotificationFilterResult : BaseFilter<NotificationFilterData, NotificationFilterParam>
    { 
    }
    public class NotificationDto : BaseDto
    {
        //public long? EventId { get;  set; }
        public bool IsSend { get;  set; }
        public bool IsActive { get;  set; } //برای وقتی که تایم پکیج تموم شد
        public DateTime EventStartTime { get;  set; }
        public DateTime EventEndTime { get;  set; }
        public DateTime SendTime { get;  set; }

        public NotificationType NotificationType { get;  set; }
        public ICollection<string>? UserNames { get;  set; }

        public EventNotificationDto? eventDto { get; set; }
    }
    public class EventNotificationDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public NotificationEnum notification { get; set; }

        public bool AccessNotification { get; set; } = true;
    }
}

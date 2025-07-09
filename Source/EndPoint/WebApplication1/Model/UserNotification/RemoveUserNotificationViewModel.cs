using Common.Application;
using Common.Query.Filter;
using Domain.NotificationAgg;
using Domain.UserAgg;

namespace Planning.Api.Model.UserNotification
{
    public record class RemoveUserNotificationViewModel(long UserNotificationId);
    public class UserNotificationFilterParamForAdminViewModel 
    {
        public int PageId { get; set; } = 1;
        public int Take { get; set; } = 10;
        public string? Search { get; set; }
    }
    public class UserNotificationFilterParamViewModel : BaseFilterParam
    {
        public string? Search { get; set; }
    }
    public class MarkAsReadUserNotificationViewModel
    {
        public long UserNotificationId { get; set; }
    }
    public class AddUserNotificationCommandViewModel
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; } //ارسال بشه یانه
        public DateTime SendTime { get; set; }
        public bool SendToAllUser { get; set; }
        public required UserNotificationType NotificationType { get; set; }

        public List<string>? UserIds { get; set; }
    }

}

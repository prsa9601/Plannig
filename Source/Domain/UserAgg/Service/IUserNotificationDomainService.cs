
namespace Domain.UserAgg.Service
{
    public interface IUserNotificationDomainService
    {
        Task SendNotification(long UserNotificationId);
        Task SendSms(long UserNotificationId);
        Task SendEmail(long UserNotificationId);
    }
}

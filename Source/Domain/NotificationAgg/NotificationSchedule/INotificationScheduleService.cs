namespace Domain.NotificationAgg.NotificationSchedule
{
    public interface INotificationScheduleService
    {
        Task<bool> Add(DateTime eventStartTime
            , DateTime eventEndTime, DateTime sendTime, long notificationId,
            Dictionary<string, string> usersDetails, string eventDescription,
            string eventTitle, string creatorUserName, string creatorUserEmail);
        Task<bool> Update(long notificationId,
            DateTime eventStartTime, DateTime eventEndTime, DateTime newSendTime,
            Dictionary<string, string> usersDetails, string eventDescription,
            string eventTitle, string creatorUserName, string creatorUserEmail);
        Task<bool> Remove(long notificationId);
    }
}

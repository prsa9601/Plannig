using Domain.NotificationAgg.NotificationSchedule;

namespace Application.NotificationSchedule
{
    public class NotificationScheduleService : INotificationScheduleService
    {
        private readonly EventNotificationScheduler _schedule;

        public NotificationScheduleService(EventNotificationScheduler schedule)
        {
            _schedule = schedule;
        }

        public async Task<bool> Add(DateTime eventStartTime
            , DateTime eventEndTime, DateTime sendTime, long notificationId,
            Dictionary<string, string> usersDetails, string eventDescription,
            string eventTitle, string creatorUserName, string creatorUserEmail)
        {
           return await _schedule.AddScheduleEvent(eventStartTime,
                eventEndTime, sendTime, notificationId, usersDetails, 
                eventDescription, eventTitle, creatorUserName, creatorUserEmail);
        }

        public async Task<bool> Update(long notificationId,
            DateTime eventStartTime, DateTime eventEndTime, DateTime newSendTime,
            Dictionary<string, string> usersDetails, string eventDescription,
            string eventTitle, string creatorUserName, string creatorUserEmail)
        {
            return await _schedule.UpdateEvent(notificationId, eventStartTime,
                eventEndTime, newSendTime, usersDetails, eventDescription, eventTitle,
                creatorUserName, creatorUserEmail);
        }

        public async Task<bool> Remove(long notificationId)
        { 
            return await _schedule.DeleteEvent(notificationId);
        }
    }
}

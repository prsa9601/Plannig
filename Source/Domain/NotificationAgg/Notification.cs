using Common.Domain;
using Domain.NotificationAgg.Service;

namespace Domain.NotificationAgg
{
    public class Notification : BaseEntity
    {
        public Notification(long eventId, bool isSend, bool isSeen,
            DateTime eventSendTime,
            NotificationType notificationType,
            ICollection<string>? userNames, bool isActive, string title, string description)
        {
            EventId = eventId;
            IsSend = isSend;
            IsSeen = isSeen;
            //AllowedEmailCount = allowedEmailCount;
            //AllowedSmsCount = allowedSmsCount;
            EventStartTime = eventSendTime;
            //EventExpiredTime = eventExpiredTime;
            //SendTime = sendTime;
            NotificationType = notificationType;
            UserIds = userNames;
            IsActive = isActive;
            Title = title;
            Description = description;
            //ScheduleId = scheduleId;, string scheduleId
        }

        private Notification()
        {
            UserIds = new List<string>();
            IsSeen = false;
            IsSend = false;
            IsActive = false;
        }
        public long? EventId { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ScheduleId { get; set; } = "";
        public string NotificationScheduleId { get; set; } = "";
        public bool IsSend { get; private set; }
        public bool IsActive { get; private set; } //برای وقتی که تایم پکیج تموم شد
        public bool IsSeen { get; private set; }
        public int AllowedEmailCount { get; private set; }
        public int AllowedSmsCount { get; private set; }
        public DateTime EventStartTime { get; private set; }
        public DateTime EventEndTime { get; private set; }
        public DateTime SendTime { get; private set; }

        public NotificationType NotificationType { get; private set; }
        public ICollection<string>? UserIds { get; private set; }



        public void Add(long eventId, bool isSend, bool isSeen,
            int allowedEmailCount, int allowedSmsCount,
            DateTime eventSendTime, DateTime eventEndTime,
            DateTime sendTime, NotificationType notificationType,
            ICollection<string> userIds)
        {
            EventId = eventId;
            IsSend = isSend;
            IsSeen = isSeen;
            AllowedEmailCount = allowedEmailCount;
            AllowedSmsCount = allowedSmsCount;
            EventStartTime = eventSendTime;
            EventEndTime = eventEndTime;
            SendTime = sendTime;
            NotificationType = notificationType;
            UserIds = userIds;
        }

        public void Edit(long eventId, bool isSend, bool isSeen,
            DateTime eventSendTime,
            NotificationType notificationType,
            ICollection<string>? userIds, bool isActive, DateTime endTime, string title,
            string description)
        {
            EventId = eventId;
            IsSend = isSend;
            Title = title;
            Description = description;
            IsSeen = isSeen;
            //AllowedEmailCount = allowedEmailCount;
            //AllowedSmsCount = allowedSmsCount;
            EventStartTime = eventSendTime;
            EventEndTime = endTime;
            //EventExpiredTime = eventExpiredTime;
            //SendTime = sendTime;
            NotificationType = notificationType;
            UserIds = userIds;
            IsActive = isActive;
            //ScheduleId = scheduleId;, string scheduleId
        }
        public void ChangeDate(DateTime eventStartTime,
            DateTime sendTime, DateTime endTime)
        {

            //AllowedEmailCount = allowedEmailCount;
            //AllowedSmsCount = allowedSmsCount;
            EventStartTime = eventStartTime;
            EventEndTime = endTime;
            //EventExpiredTime = eventExpiredTime;
            SendTime = sendTime;
            //ScheduleId = scheduleId;, string scheduleId
        }

        public void RemoveUser(string userId)
        {
            UserIds.Remove(userId);
        }
        public void DisabledActive()
        {
            IsActive = false;
        }
        public void MarkAsSeen()
        {
            IsSeen = true;
        }
        public void EnabledActive()
        {
            IsActive = true;
        }
        public void ActiveSend()
        {
            IsSend = true;
        }
        public void AddUser(ICollection<string> userIds)
        {
            UserIds.Clear();
            foreach (var item in userIds)
            {
                UserIds.Add(item);
            }
        }

        //  هم میتونم با ریپازیتوری بزنمش هم با سرویس
        public void SendEmailForEvent(List<string> userIds, long eventId,
            DateTime eventStartTime, DateTime eventEndTime, bool isSend,
            int allowedEmailCount, bool isActive, string creatorUserId)
        {
            GuardSendEmail(isSend, isActive, allowedEmailCount);
            //service.SendEmailForEvent(userIds, eventId, eventStartTime,
            //    eventEndTime, creatorUserName);
        }
        //public void SendEmailForEvent(List<string> userIds, long eventId,
        //   DateTime eventStartTime, DateTime eventEndTime, bool isSend,
        //   int allowedEmailCount, bool isActive, string creatorUserName,
        //   INotificationService service)
        //{
        //    GuardSendEmail(isSend, isActive, allowedEmailCount);
        //    service.SendEmailForEvent(userIds, eventId, eventStartTime,
        //        eventEndTime, creatorUserName);
        //}
        public void SendEmail(List<string>? userIds, long eventId,
            DateTime eventStartTime, DateTime eventEndTime, bool isSend,
            int allowedEmailCount, bool isActive, INotificationService service)
        {
            GuardSendEmail(isSend, isActive, allowedEmailCount);
            service.SendEmail(userIds, eventId, eventStartTime, eventEndTime);
        }
        public void SendSms(List<string> userIds, long eventId,
            DateTime eventStartTime, DateTime eventEndTime, bool isSend,
            int allowedSmsCount, bool isActive, INotificationService service)
        {
            GuardSendSms(isSend, isActive, allowedSmsCount);
            service.SendSms(userIds, eventId, eventStartTime, eventEndTime);
        }

        private void CountEmailControl(List<string>? userIds)
        {

        }
        private void CountSmsControl()
        {

        }
        private void GuardSendEmail(bool isSend, bool isActive, int allowedEmailCount)
        {
            if (AllowedEmailCount <= 0)
                throw new InvalidOperationException
                    ("شما در حال حاضر قادر به استفاده از ارسال ایمیل نیستید! ");

            if (!isActive)
                throw new InvalidOperationException
                    ("شما در حال حاضر قادر به استفاده از ارسال ایمیل نیستید! ");

            if (IsSend)
                throw new InvalidOperationException("نوتیفیکیشن از طریق ایمیل یکبار ارسال شده!");
        }
        private void GuardSendSms(bool isSend, bool isActive, int allowedSmsCount)
        {
            if (AllowedEmailCount <= 0)
                throw new Exception
                    ("شما در حال حاضر قادر به استفاده از ارسال پیامک نیستید! ");

            if (!isActive)
                throw new Exception
                    ("شما در حال حاضر قادر به استفاده از ارسال پیامک نیستید! ");

            if (IsSend)
                throw new Exception("نوتیفیکیشن از طریق پیامک یکبار ارسال شده!");
        }

    }
    [Flags]
    public enum NotificationType
    {
        None,
        Email,
        Sms
    }
}
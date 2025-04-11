using System.Data;
using Common.Domain;
using Domain.Notification.Service;

namespace Domain.Notification
{
    //برای ارسال نوتیفیکیشن میتونم از RabbitMQ یا دیتابیس استفاده کنم
    public class Notification : BaseEntity
    {
        public Notification(long eventId, bool isSend, bool isSeen, 
            DateTime eventSendTime, 
            DateTime sendTime, NotificationType notificationType,
            ICollection<string>? userNames, bool isActive)
        {
            EventId = eventId;
            IsSend = isSend;
            IsSeen = isSeen;
            //AllowedEmailCount = allowedEmailCount;
            //AllowedSmsCount = allowedSmsCount;
            EventStartTime = eventSendTime;
            //EventExpiredTime = eventExpiredTime;
            SendTime = sendTime;
            NotificationType = notificationType;
            UserNames = userNames;
            IsActive = isActive;
            //ScheduleId = scheduleId;, string scheduleId
        }
        
        private Notification()
        {
            UserNames = new List<string>();
            IsSeen = false;
            IsSend = false;
            IsActive = false;
        }
        public long? EventId { get; private set; }
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
        public ICollection<string>? UserNames { get; private set; }



        public void Add (long eventId, bool isSend, bool isSeen,
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
            UserNames = userIds;
        }

        public void Edit(long eventId, bool isSend, bool isSeen,
            DateTime eventSendTime,
            DateTime sendTime, NotificationType notificationType,
            ICollection<string>? userNames, bool isActive, DateTime endTime)
        {
            EventId = eventId;
            IsSend = isSend;
            IsSeen = isSeen;
            //AllowedEmailCount = allowedEmailCount;
            //AllowedSmsCount = allowedSmsCount;
            EventStartTime = eventSendTime;
            EventEndTime = endTime;
            //EventExpiredTime = eventExpiredTime;
            SendTime = sendTime;
            NotificationType = notificationType;
            UserNames = userNames;
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
            UserNames.Remove(userId);
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
            UserNames.Clear();
            foreach (var item in userIds)
            {
                UserNames.Add(item);
            }
        }

        //  هم میتونم با ریپازیتوری بزنمش هم با سرویس
        public void SendEmailForEvent(List<string> userIds, long eventId,
            DateTime eventStartTime, DateTime eventEndTime, bool isSend,
            int allowedEmailCount, bool isActive, string creatorUserName)
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
        public void SendEmail(List<string>? userNames, long eventId,
            DateTime eventStartTime, DateTime eventEndTime, bool isSend,
            int allowedEmailCount, bool isActive, INotificationService service)
        {
            GuardSendEmail(isSend, isActive, allowedEmailCount);
            service.SendEmail(userNames, eventId, eventStartTime, eventEndTime);
        }
        public void SendSms(List<string> userNames, long eventId, 
            DateTime eventStartTime, DateTime eventEndTime, bool isSend, 
            int allowedSmsCount, bool isActive, INotificationService service)
        {
            GuardSendSms(isSend, isActive, allowedSmsCount);
            service.SendSms(userNames, eventId, eventStartTime, eventEndTime);
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

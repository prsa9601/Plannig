using Common.Domain;
using System.ComponentModel;

namespace Domain.UserAgg
{
    public class UserNotification : BaseEntity
    {
        public UserNotification(string title, string description,
            bool isActive, DateTime sendTime, List<string> userIds,
            UserNotificationType sendType)
        {
            Title = title;
            Description = description;
            IsSend = false;
            IsSeen = false;
            SendTime = sendTime;    
            UserIds = userIds;
            SendType = sendType;
            IsActive = isActive;
            SendType = sendType;
        }

        public ICollection<string>? UserIds { get; private set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public bool IsSend { get; set; }
        public bool IsSeen { get; set; }
        public DateTime SendTime { get; private set; }

        public bool IsActive { get; set; } //ارسال بشه یانه

        public UserNotificationType SendType { get; set; }

        private UserNotification()
        {
            IsSeen = false;
            IsSend = false;
        }

        public void Edit(string title, string description, bool isSend,
            bool isActive, bool isSeen, DateTime sendTime, UserNotificationType sendType)
        {
            Title = title;
            Description = description;
            IsSend = isSend;
            IsSeen = isSeen;
            IsActive = isActive;
            SendType = sendType;
        }
        public void ChangeDate(DateTime sendTime)
        {
            SendTime = sendTime;
        }

        public void SendNotification()
        {
            IsSend = true;
        }
        public void SeenNotification()
        {
            IsSeen = true;
        }
        //public void ChangeActivityStatus()
        //{
        //    IsActive = true ? IsActive = false : IsActive = true;
        //}
        public void EnabledActive() => IsActive = true ? IsActive = false : IsActive = true;
    }
    [Flags]
    public enum UserNotificationType
    {
        [Description("هیچکدام")]
        None = 0,

        [Description("وبسایت")]
        Website = 1,

        [Description("ایمیل")]
        Email = 2,

        [Description("پیامک")]
        Sms = 3

        //    [Description("هیچکدام")]
        //None = 0,          // 0000

        //[Description("وبسایت")]
        //Website = 1,       // 0001

        //[Description("ایمیل")]
        //Email = 1 << 1,    // 0010 (یا 2)

        //[Description("پیامک")]
        //Sms = 1 << 2       // 0100 (یا 4)
    }
}

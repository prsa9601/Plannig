using System.Reflection.Emit;
using Common.Domain;
using Common.Domain.Exceptions;
using Domain.EventAgg.Enum;
using Domain.UserAgg;

namespace Domain.EventAgg
{
    public class Event : BaseEntity
    {
        public string Title { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Description { get; private set; }
        public string Link { get; private set; }
        public string EventAddress { get; private set; }

        public Tagged Tag { get; private set; }
        public List<EventUser> EventUser { get; set; } = new List<EventUser>();
        public NotificationEnum notification { get; private set; }
        //public List<UserEvent> Participants { get; private set; }

        public bool AccessNotification { get; set; } = true;

        public void DisableAccessNotification()
        {
            AccessNotification = false;
        }
        public void EnableAccessNotification()
        {
            AccessNotification = true;
        }

        public void AddUser(List<string> UserIds)
        {
            List<EventUser> users = new List<EventUser>();

            foreach (var item in UserIds)
            {
                users.Add(new EventUser(item));
            }
            users.ForEach(f => f.EventId = Id);

            //eventUser.Clear();
            EventUser.Clear();
            EventUser.AddRange(users);
        }
        public void AddUser(string UserId)
        {
            var user = new EventUser(UserId);
            user.EventId = Id;
            EventUser.Add(user);
        }

        public Event(string title, DateTime startTime, DateTime endTime,
            string description, string link, string eventAddress, Tagged tag, 
            NotificationEnum notification, bool accessNotification)
        {
            Guard(title, startTime, endTime);

            Title = title;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
            Link = link;
            EventAddress = eventAddress;
            this.Tag = tag;
            this.notification = notification;
            AccessNotification = accessNotification;
            //EventUser = new List<EventUser>();

            //Participants = new List<UserEvent>();
            //Participants = new List<EventParticipants>();
        }
        public Event(string creatorUserId, List<string>? UserIds, string title,
            DateTime startTime, DateTime endTime, string description, string link,
            string eventAddress,
            Tagged tag, NotificationEnum notification, bool accessNotification)
        {
            Guard(title, startTime, endTime);

            Title = title;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
            Link = link;
            EventAddress = eventAddress;
            this.Tag = tag;
            this.notification = notification;
            AccessNotification = accessNotification;
            EventUser = new List<EventUser>();

            //Participants = new List<UserEvent>();
            //Participants = new List<EventParticipants>();
        }

        public void AddEventUser(string CreatorUserId)
        {
            EventUser user = new EventUser(CreatorUserId);
            user.EventId = Id;
            user.CreatorUserId = CreatorUserId;
            //eventUser.Clear();
            //eventUser.Clear();
            EventUser.Add(user);
        }
        public void AddEventUser(string CreatorUserId, List<string> UserIds)
        {
            List<EventUser> users = new List<EventUser>();

            foreach (var item in UserIds)
            {
                users.Add(new EventUser(item));
            }
            users.Add(new EventUser(CreatorUserId));
            users.ForEach(f => f.EventId = Id);
            users.ForEach(f => f.CreatorUserId = CreatorUserId);
            //eventUser.Clear();
            //eventUser.Clear();
            EventUser.AddRange(users);
        }
        public void RemoveUserAsFromEvent(string UserId)
        {
            var eventUser = EventUser.FirstOrDefault(f => f.UserId.Equals(UserId) && f.EventId.Equals(Id));
            if (eventUser != null)
            {
                EventUser.Remove(eventUser);
            }
        }
        //public Event(string title, DateTime startTime, DateTime endTime, string description)
        //{    

        //    Title = title;
        //    StartTime = startTime;
        //    EndTime = endTime;
        //    Description = description;



        //}
        public void SetDates(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }
        public void Edit(string creatorUserName, List<string> userNumber, string title, DateTime startTime, DateTime endTime, string description, string link, string eventAddress, bool accessNotification, Tagged tag, NotificationEnum notification)
        {
            Guard(title, startTime, endTime);

            Title = title;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
            Link = link;
            EventAddress = eventAddress;
            this.Tag = tag;
            this.notification = notification;
            AccessNotification = accessNotification;

            //if (notification != NotificationEnum.none/* || userNumber.Count != 0*/)
            //{
                List<EventUser> users = new List<EventUser>();

                foreach (var item in userNumber)
                {
                    users.Add(new EventUser(item));
                }
                users.Add(new EventUser(creatorUserName));
                users.ForEach(f => f.EventId = Id);
                users.ForEach(i => i.CreatorUserId = creatorUserName);
                //eventUser.Clear();
                EventUser.Clear();
                EventUser.AddRange(users);
            //}
            //Participants = new List<UserEvent>();
            //Participants = new List<EventParticipants>();
        }
        //public void Edit(string title, DateTime startTime, DateTime endTime, string description)
        //{
        //    Guard(title, startTime, endTime);


        //    Title = title;
        //    StartTime = startTime;
        //    EndTime = endTime;
        //    Description = description;

        //    //List<UserEvent> participants = new List<UserEvent>();

        //    //foreach (var item in eventId)
        //    //{
        //    //    participants.Add(new UserEvent(item));, List<long> eventId
        //    //}
        //    //participants.ForEach(f => f.UserId = Id);

        //    //Participants.Clear();
        //    //Participants.AddRange(participants);

        //}



        public void Guard(
           string title, DateTime startDate, DateTime endDate)
        {

            if (string.IsNullOrWhiteSpace(title))
                throw new InvalidDomainDataException(" نام نامعتبر است");

            if (startDate >= endDate)
                throw new InvalidDomainDataException("تاریخ شروع و پایان مصابقت ندارند!");


        }
    }
}

using Common.Query;
using Domain.EventAgg.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Event.DTOs
{
    public class EventDto : BaseDto
    {
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string postId { get; set; } //InstagramPostId OR TelegramPostId
        public string EventAddress { get; set; }

        public Tagged tag { get; set; }
        public Notification notification { get; set; }
        //public List<UserEvent> Participants { get; private set; }

        public bool AccessNotification { get; set; } = true;
    }
    public class EventForShopDto : BaseDto
    {
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Tagged tag { get; set; }

    }
}

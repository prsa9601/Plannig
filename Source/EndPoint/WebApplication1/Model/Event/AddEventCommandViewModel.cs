using Domain.EventAgg.Enum;

namespace Planning.Api.Model.Event
{
    public class AddEventCommandViewModel
    {
            public string Title { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            //public string StartTime { get; set; }
            //public string EndTime { get; set; }
            public string Description { get; set; }
            public string Link { get; set; }
            public string EventAddress { get; set; }
            public bool accessNotification { get; set; }

            public Tagged tag { get; set; }
            public List<string> userNames { get; set; }
            public Domain.EventAgg.Enum.NotificationEnum notification { get; set; }
        
    }

    public class EditEventCommandViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string EventAddress { get; set; }
        public bool accessNotification { get; set; }
        //public string creatorUserName { get; set; }


        public List<string> userNames { get; set; }
        public Tagged tag { get; set; }
        public Domain.EventAgg.Enum.NotificationEnum NotificationEnum { get; set; }
    }
}
